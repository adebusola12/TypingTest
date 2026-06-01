window.WordDropAudio = (() => {

    let ctx = null;
    let masterGain = null;
    let isPlaying = false;
    let isMuted = false;
    let schedulerTimer = null;
    let currentStep = 0;
    let nextStepTime = 0;
    let bpm = 95;

    const LOOKAHEAD_MS = 25;
    const SCHEDULE_AHEAD = 0.1;

    function getCtx() {
        if (!ctx) {
            ctx = new (window.AudioContext || window.webkitAudioContext)();
            masterGain = ctx.createGain();
            masterGain.gain.value = 0.45;
            masterGain.connect(ctx.destination);
        }
        return ctx;
    }

    function resume() {
        const c = getCtx();
        if (c.state === 'suspended') c.resume();
    }

    // ── Kick — low thud ──────────────────────────────────────────────────────
    function playKick(time) {
        const c = getCtx();
        const osc = c.createOscillator();
        const gain = c.createGain();
        osc.frequency.setValueAtTime(150, time);
        osc.frequency.exponentialRampToValueAtTime(0.001, time + 0.4);
        gain.gain.setValueAtTime(0.9, time);
        gain.gain.exponentialRampToValueAtTime(0.001, time + 0.4);
        osc.connect(gain); gain.connect(masterGain);
        osc.start(time); osc.stop(time + 0.4);
    }

    // ── Snare — noise burst ───────────────────────────────────────────────────
    function playSnare(time) {
        const c = getCtx();
        const dur = 0.15;
        const buf = c.createBuffer(1, Math.floor(c.sampleRate * dur), c.sampleRate);
        const data = buf.getChannelData(0);
        for (let i = 0; i < data.length; i++) data[i] = Math.random() * 2 - 1;
        const src = c.createBufferSource();
        src.buffer = buf;
        const hp = c.createBiquadFilter();
        hp.type = 'highpass';
        hp.frequency.value = 2000;
        const gain = c.createGain();
        gain.gain.setValueAtTime(0.5, time);
        gain.gain.exponentialRampToValueAtTime(0.001, time + dur);
        src.connect(hp); hp.connect(gain); gain.connect(masterGain);
        src.start(time); src.stop(time + dur);

        // Snare body tone
        const osc = c.createOscillator();
        const og = c.createGain();
        osc.frequency.value = 200;
        og.gain.setValueAtTime(0.35, time);
        og.gain.exponentialRampToValueAtTime(0.001, time + 0.08);
        osc.connect(og); og.connect(masterGain);
        osc.start(time); osc.stop(time + 0.08);
    }

    // ── Hi-Hat ───────────────────────────────────────────────────────────────
    function playHat(time, open = false) {
        const c = getCtx();
        const dur = open ? 0.12 : 0.04;
        const vol = open ? 0.18 : 0.1;
        const buf = c.createBuffer(1, Math.floor(c.sampleRate * dur), c.sampleRate);
        const d = buf.getChannelData(0);
        for (let i = 0; i < d.length; i++) d[i] = Math.random() * 2 - 1;
        const src = c.createBufferSource();
        src.buffer = buf;
        const bp = c.createBiquadFilter();
        bp.type = 'bandpass';
        bp.frequency.value = 9000;
        const gain = c.createGain();
        gain.gain.setValueAtTime(vol, time);
        gain.gain.exponentialRampToValueAtTime(0.001, time + dur);
        src.connect(bp); bp.connect(gain); gain.connect(masterGain);
        src.start(time); src.stop(time + dur);
    }

    // ── Bass note ─────────────────────────────────────────────────────────────
    function playBass(time, freq = 55) {
        const c = getCtx();
        const osc = c.createOscillator();
        const gain = c.createGain();
        osc.type = 'sawtooth';
        osc.frequency.value = freq;
        gain.gain.setValueAtTime(0.3, time);
        gain.gain.exponentialRampToValueAtTime(0.001, time + 0.25);
        osc.connect(gain); gain.connect(masterGain);
        osc.start(time); osc.stop(time + 0.25);
    }

    // ── Beat pattern (16 steps = 1 bar) ──────────────────────────────────────
    //  K = Kick, S = Snare, H = Hat, O = Open Hat, B = Bass
    //  Step: 0  1  2  3  4  5  6  7  8  9  10 11 12 13 14 15
    //  K:    X        X        X              X
    //  S:          X              X                 X
    //  H:    X  X  X  X  X  X  X  X  X  X  X  X  X  X  X  X
    //  B:    X              X        X
    const BASS_NOTES = [55, 55, 65, 55, 49, 55, 65, 73];

    function scheduleStep(step, time) {
        const s = step % 16;
        if (s === 0 || s === 4 || s === 8 || s === 11) playKick(time);
        if (s === 4 || s === 9 || s === 13) playSnare(time);
        playHat(time, s % 4 === 2);
        if (s === 0 || s === 4 || s === 6)
            playBass(time, BASS_NOTES[Math.floor(s / 2) % BASS_NOTES.length]);
    }

    function scheduler() {
        const c = getCtx();
        const spb = (60 / bpm) / 4; // 16th note
        while (nextStepTime < c.currentTime + SCHEDULE_AHEAD) {
            if (!isMuted) scheduleStep(currentStep, nextStepTime);
            nextStepTime += spb;
            currentStep++;
        }
        schedulerTimer = setTimeout(scheduler, LOOKAHEAD_MS);
    }

    // ── Sound Effects ─────────────────────────────────────────────────────────

    // Word destroyed — bright pop
    function playDestroy() {
        if (isMuted) return;
        const c = getCtx();
        resume();
        [880, 1100].forEach((freq, i) => {
            const osc = c.createOscillator();
            const gain = c.createGain();
            const t = c.currentTime + i * 0.03;
            osc.type = 'triangle';
            osc.frequency.value = freq;
            gain.gain.setValueAtTime(0.3, t);
            gain.gain.exponentialRampToValueAtTime(0.001, t + 0.15);
            osc.connect(gain); gain.connect(masterGain);
            osc.start(t); osc.stop(t + 0.15);
        });
    }

    // Word missed — low thud
    function playMiss() {
        if (isMuted) return;
        const c = getCtx();
        resume();
        const osc = c.createOscillator();
        const gain = c.createGain();
        osc.type = 'sawtooth';
        osc.frequency.setValueAtTime(180, c.currentTime);
        osc.frequency.exponentialRampToValueAtTime(50, c.currentTime + 0.3);
        gain.gain.setValueAtTime(0.4, c.currentTime);
        gain.gain.exponentialRampToValueAtTime(0.001, c.currentTime + 0.3);
        osc.connect(gain); gain.connect(masterGain);
        osc.start(c.currentTime); osc.stop(c.currentTime + 0.3);
    }

    // Wrong word — error buzz
    function playWrong() {
        if (isMuted) return;
        const c = getCtx();
        resume();
        const osc = c.createOscillator();
        const gain = c.createGain();
        osc.type = 'square';
        osc.frequency.value = 120;
        gain.gain.setValueAtTime(0.25, c.currentTime);
        gain.gain.exponentialRampToValueAtTime(0.001, c.currentTime + 0.2);
        osc.connect(gain); gain.connect(masterGain);
        osc.start(c.currentTime); osc.stop(c.currentTime + 0.2);
    }

    // Wave start — ascending sweep
    function playWave(waveNum) {
        if (isMuted) return;
        const c = getCtx();
        resume();
        const baseFreqs = [330, 392, 440, 523, 587];
        baseFreqs.forEach((freq, i) => {
            const osc = c.createOscillator();
            const gain = c.createGain();
            const t = c.currentTime + i * 0.1;
            osc.type = 'triangle';
            osc.frequency.value = freq * (1 + (waveNum - 1) * 0.05);
            gain.gain.setValueAtTime(0.25, t);
            gain.gain.exponentialRampToValueAtTime(0.001, t + 0.2);
            osc.connect(gain); gain.connect(masterGain);
            osc.start(t); osc.stop(t + 0.2);
        });
    }

    // Game start
    function playStart() {
        if (isMuted) return;
        const c = getCtx();
        resume();
        [220, 330, 440, 550].forEach((freq, i) => {
            const osc = c.createOscillator();
            const gain = c.createGain();
            const t = c.currentTime + i * 0.08;
            osc.type = 'triangle';
            osc.frequency.value = freq;
            gain.gain.setValueAtTime(0.25, t);
            gain.gain.exponentialRampToValueAtTime(0.001, t + 0.15);
            osc.connect(gain); gain.connect(masterGain);
            osc.start(t); osc.stop(t + 0.15);
        });
    }

    // ── BPM scales with wave ──────────────────────────────────────────────────
    function bpmForWave(wave) {
        return Math.min(95 + (wave - 1) * 8, 145);
    }

    // ── Public API ────────────────────────────────────────────────────────────
    function start(wave = 1) {
        if (isPlaying) return;
        resume();
        bpm = bpmForWave(wave);
        isPlaying = true;
        currentStep = 0;
        nextStepTime = getCtx().currentTime + 0.05;
        scheduler();
    }

    function stop() {
        isPlaying = false;
        clearTimeout(schedulerTimer);
    }

    function updateWave(wave) {
        bpm = bpmForWave(wave);
    }

    function toggleMute() {
        isMuted = !isMuted;
        if (masterGain) masterGain.gain.value = isMuted ? 0 : 0.45;
        return isMuted;
    }

    return {
        start, stop, updateWave, toggleMute,
        playDestroy, playMiss, playWrong, playWave, playStart
    };

})();