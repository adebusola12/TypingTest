const SniperAudio = (() => {

    let ctx = null;
    let masterGain = null;
    let isPlaying = false;
    let bpm = 90;
    let currentBeat = 0;
    let nextBeatTime = 0;
    let schedulerTimer = null;
    let isMuted = false;

    const LOOKAHEAD_MS = 25;    // how often the scheduler fires (ms)
    const SCHEDULE_AHEAD = 0.1;  // how far ahead to schedule audio (seconds)

    // ── Context ────────────────────────────────────────────────────────────────
    function getCtx() {
        if (!ctx) {
            ctx = new (window.AudioContext || window.webkitAudioContext)();
            masterGain = ctx.createGain();
            masterGain.gain.value = 0.55;
            masterGain.connect(ctx.destination);
        }
        return ctx;
    }

    function resume() {
        const context = getCtx();
        if (context.state === 'suspended') context.resume();
    }

    // ── Drum Sounds ────────────────────────────────────────────────────────────

    // Deep thud – pitch sweeps down fast
    function playKick(time) {
        const context = getCtx();

        const osc = context.createOscillator();
        const gain = context.createGain();

        osc.frequency.setValueAtTime(160, time);
        osc.frequency.exponentialRampToValueAtTime(0.001, time + 0.45);

        gain.gain.setValueAtTime(1.0, time);
        gain.gain.exponentialRampToValueAtTime(0.001, time + 0.45);

        osc.connect(gain);
        gain.connect(masterGain);

        osc.start(time);
        osc.stop(time + 0.45);
    }

    // Filtered white noise burst
    function playSnare(time) {
        const context = getCtx();
        const duration = 0.18;

        // Noise layer
        const bufSize = Math.floor(context.sampleRate * duration);
        const buffer = context.createBuffer(1, bufSize, context.sampleRate);
        const data = buffer.getChannelData(0);
        for (let i = 0; i < bufSize; i++) data[i] = Math.random() * 2 - 1;

        const noise = context.createBufferSource();
        noise.buffer = buffer;

        const hpFilter = context.createBiquadFilter();
        hpFilter.type = 'highpass';
        hpFilter.frequency.value = 1800;

        const noiseGain = context.createGain();
        noiseGain.gain.setValueAtTime(0.7, time);
        noiseGain.gain.exponentialRampToValueAtTime(0.001, time + duration);

        noise.connect(hpFilter);
        hpFilter.connect(noiseGain);
        noiseGain.connect(masterGain);

        // Tone layer (snare body)
        const osc = context.createOscillator();
        const oscGain = context.createGain();
        osc.frequency.value = 185;
        oscGain.gain.setValueAtTime(0.6, time);
        oscGain.gain.exponentialRampToValueAtTime(0.001, time + 0.1);

        osc.connect(oscGain);
        oscGain.connect(masterGain);

        noise.start(time); noise.stop(time + duration);
        osc.start(time); osc.stop(time + 0.1);
    }

    // Short high-frequency noise tick
    function playHiHat(time, accent = false) {
        const context = getCtx();
        const duration = accent ? 0.08 : 0.04;
        const volume = accent ? 0.25 : 0.12;

        const bufSize = Math.floor(context.sampleRate * duration);
        const buffer = context.createBuffer(1, bufSize, context.sampleRate);
        const data = buffer.getChannelData(0);
        for (let i = 0; i < bufSize; i++) data[i] = Math.random() * 2 - 1;

        const source = context.createBufferSource();
        source.buffer = buffer;

        const bp = context.createBiquadFilter();
        bp.type = 'bandpass';
        bp.frequency.value = 10000;
        bp.Q.value = 0.8;

        const gain = context.createGain();
        gain.gain.setValueAtTime(volume, time);
        gain.gain.exponentialRampToValueAtTime(0.001, time + duration);

        source.connect(bp);
        bp.connect(gain);
        gain.connect(masterGain);

        source.start(time);
        source.stop(time + duration);
    }

    // ── Beat Pattern ───────────────────────────────────────────────────────────
    //
    //  Step:  0    1    2    3    4    5    6    7
    //  Kick:  X              X
    //  Snare:       X              X
    //  Hat:   X    X    X    X    X    X    X    X   (accent on 0 & 4)
    //
    function scheduleBeat(step, time) {
        const s = step % 8;

        if (s === 0 || s === 3) playKick(time);
        if (s === 2 || s === 6) playSnare(time);
        playHiHat(time, s === 0 || s === 4);
    }

    // ── Scheduler ──────────────────────────────────────────────────────────────
    function scheduler() {
        const context = getCtx();
        const secondsPerStep = (60 / bpm) / 2;   // 2 steps per beat = 8th notes

        while (nextBeatTime < context.currentTime + SCHEDULE_AHEAD) {
            if (!isMuted) scheduleBeat(currentBeat, nextBeatTime);
            nextBeatTime += secondsPerStep;
            currentBeat++;
        }
        schedulerTimer = setTimeout(scheduler, LOOKAHEAD_MS);
    }

    // ── BPM Ramp ───────────────────────────────────────────────────────────────
    //  Called whenever the streak bucket changes so the beat feels like
    //  it's responding to the player's performance.
    function bpmForStreak(streak) {
        if (streak >= 15) return 145;
        if (streak >= 10) return 128;
        if (streak >= 7) return 115;
        if (streak >= 4) return 100;
        return 88;
    }

    // ── Sound Effects ──────────────────────────────────────────────────────────

    // Crisp high ping on correct word
    function playHit() {
        if (isMuted) return;
        const context = getCtx();
        resume();

        const osc = context.createOscillator();
        const gain = context.createGain();

        osc.type = 'sine';
        osc.frequency.setValueAtTime(1200, context.currentTime);
        osc.frequency.exponentialRampToValueAtTime(600, context.currentTime + 0.12);

        gain.gain.setValueAtTime(0.45, context.currentTime);
        gain.gain.exponentialRampToValueAtTime(0.001, context.currentTime + 0.14);

        osc.connect(gain);
        gain.connect(masterGain);

        osc.start(context.currentTime);
        osc.stop(context.currentTime + 0.15);
    }

    // Low grinding buzz on miss
    function playMiss() {
        if (isMuted) return;
        const context = getCtx();
        resume();

        const osc = context.createOscillator();
        const gain = context.createGain();

        osc.type = 'sawtooth';
        osc.frequency.setValueAtTime(220, context.currentTime);
        osc.frequency.exponentialRampToValueAtTime(55, context.currentTime + 0.35);

        gain.gain.setValueAtTime(0.4, context.currentTime);
        gain.gain.exponentialRampToValueAtTime(0.001, context.currentTime + 0.35);

        osc.connect(gain);
        gain.connect(masterGain);

        osc.start(context.currentTime);
        osc.stop(context.currentTime + 0.35);
    }

    // Descending four-note phrase on game over
    function playGameOver() {
        if (isMuted) return;
        const context = getCtx();
        resume();

        [440, 370, 311, 220].forEach((freq, i) => {
            const osc = context.createOscillator();
            const gain = context.createGain();
            const t = context.currentTime + i * 0.22;

            osc.type = 'sine';
            osc.frequency.value = freq;

            gain.gain.setValueAtTime(0.4, t);
            gain.gain.exponentialRampToValueAtTime(0.001, t + 0.3);

            osc.connect(gain);
            gain.connect(masterGain);

            osc.start(t);
            osc.stop(t + 0.3);
        });
    }

    // Short rising blip on game start
    function playStart() {
        if (isMuted) return;
        const context = getCtx();
        resume();

        [330, 440, 550].forEach((freq, i) => {
            const osc = context.createOscillator();
            const gain = context.createGain();
            const t = context.currentTime + i * 0.1;

            osc.type = 'triangle';
            osc.frequency.value = freq;

            gain.gain.setValueAtTime(0.35, t);
            gain.gain.exponentialRampToValueAtTime(0.001, t + 0.12);

            osc.connect(gain);
            gain.connect(masterGain);

            osc.start(t);
            osc.stop(t + 0.12);
        });
    }

    // ── Public API ─────────────────────────────────────────────────────────────
    function start(streak = 0) {
        if (isPlaying) return;
        resume();
        bpm = bpmForStreak(streak);
        isPlaying = true;
        currentBeat = 0;
        nextBeatTime = getCtx().currentTime + 0.05;
        scheduler();
    }

    function stop() {
        isPlaying = false;
        clearTimeout(schedulerTimer);
    }

    function updateBpm(streak) {
        bpm = bpmForStreak(streak);
    }

    function toggleMute() {
        isMuted = !isMuted;
        if (masterGain) masterGain.gain.value = isMuted ? 0 : 0.55;
        return isMuted;
    }

    return { start, stop, updateBpm, toggleMute, playHit, playMiss, playGameOver, playStart, bpmForStreak };

})();