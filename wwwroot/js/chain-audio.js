window.ChainAudio = (() => {

    let ctx = null;
    let masterGain = null;
    let isPlaying = false;
    let bpm = 80;
    let currentStep = 0;
    let nextStepTime = 0;
    let schedulerTimer = null;
    let isMuted = false;
    let onBeatCallback = null;
    let arpeggioIndex = 0;

    const LOOKAHEAD_MS = 25;
    const SCHEDULE_AHEAD = 0.12;

    const PENTA = [130.81, 146.83, 164.81, 196.00, 220.00, 261.63, 293.66, 329.63];

    function getCtx() {
        if (!ctx) {
            ctx = new (window.AudioContext || window.webkitAudioContext)();
            masterGain = ctx.createGain();
            masterGain.gain.value = 0.5;
            masterGain.connect(ctx.destination);
        }
        return ctx;
    }

    function resume() {
        const c = getCtx();
        if (c.state === 'suspended') c.resume();
    }

    function playBassPulse(time) {
        const c = getCtx();
        const osc = c.createOscillator();
        const gain = c.createGain();
        osc.type = 'sine';
        osc.frequency.setValueAtTime(65, time);
        osc.frequency.exponentialRampToValueAtTime(40, time + 0.3);
        gain.gain.setValueAtTime(0.55, time);
        gain.gain.exponentialRampToValueAtTime(0.001, time + 0.35);
        osc.connect(gain);
        gain.connect(masterGain);
        osc.start(time);
        osc.stop(time + 0.35);
    }

    function playArpNote(time, freqOverride = null) {
        const c = getCtx();
        const freq = freqOverride ?? PENTA[arpeggioIndex % PENTA.length];
        const osc = c.createOscillator();
        const gain = c.createGain();
        const osc2 = c.createOscillator();
        const gain2 = c.createGain();
        osc.type = 'triangle';
        osc.frequency.value = freq;
        osc2.type = 'sine';
        osc2.frequency.value = freq * 2;
        gain.gain.setValueAtTime(0.22, time);
        gain.gain.exponentialRampToValueAtTime(0.001, time + 0.5);
        gain2.gain.setValueAtTime(0.06, time + 0.05);
        gain2.gain.exponentialRampToValueAtTime(0.001, time + 0.6);
        osc.connect(gain); gain.connect(masterGain);
        osc2.connect(gain2); gain2.connect(masterGain);
        osc.start(time); osc.stop(time + 0.5);
        osc2.start(time); osc2.stop(time + 0.6);
        arpeggioIndex++;
    }

    function playShimmer(time) {
        const c = getCtx();
        const duration = 0.03;
        const buf = c.createBuffer(1, Math.floor(c.sampleRate * duration), c.sampleRate);
        const data = buf.getChannelData(0);
        for (let i = 0; i < data.length; i++) data[i] = Math.random() * 2 - 1;
        const src = c.createBufferSource();
        src.buffer = buf;
        const bp = c.createBiquadFilter();
        bp.type = 'highpass';
        bp.frequency.value = 8000;
        const gain = c.createGain();
        gain.gain.setValueAtTime(0.06, time);
        gain.gain.exponentialRampToValueAtTime(0.001, time + duration);
        src.connect(bp);
        bp.connect(gain);
        gain.connect(masterGain);
        src.start(time);
        src.stop(time + duration);
    }

    function scheduleStep(step, time) {
        const s = step % 8;
        if (s === 0 || s === 4) playBassPulse(time);
        if (s % 2 === 0) playArpNote(time);
        playShimmer(time);
        if (s % 2 === 0 && onBeatCallback) {
            const delay = (time - getCtx().currentTime) * 1000;
            setTimeout(onBeatCallback, Math.max(0, delay));
        }
    }

    function scheduler() {
        const c = getCtx();
        const secondsPerStep = (60 / bpm) / 2;
        while (nextStepTime < c.currentTime + SCHEDULE_AHEAD) {
            if (!isMuted) scheduleStep(currentStep, nextStepTime);
            nextStepTime += secondsPerStep;
            currentStep++;
        }
        schedulerTimer = setTimeout(scheduler, LOOKAHEAD_MS);
    }

    function bpmForChain(chain) {
        if (chain >= 20) return 130;
        if (chain >= 15) return 118;
        if (chain >= 10) return 106;
        if (chain >= 6) return 95;
        return 80;
    }

    function beatIntervalMs() {
        return (60 / bpm) * 1000;
    }

    function msToNextBeat() {
        const c = getCtx();
        const secondsPerStep = (60 / bpm) / 2;
        let t = nextStepTime;
        const now = c.currentTime;
        let step = currentStep;
        while (t < now) {
            t += secondsPerStep;
            step++;
        }
        if ((step % 2) !== 0) {
            t += secondsPerStep;
        }
        return Math.max(0, (t - now) * 1000);
    }

    function playHit(quality = 'perfect') {
        if (isMuted) return;
        const c = getCtx();
        resume();
        const freqMap = { perfect: 880, good: 660, late: 440 };
        const freq = freqMap[quality] ?? 660;
        [freq, freq * 1.5].forEach((f, i) => {
            const osc = c.createOscillator();
            const gain = c.createGain();
            const t = c.currentTime + i * 0.04;
            osc.type = 'triangle';
            osc.frequency.value = f;
            gain.gain.setValueAtTime(0.3 - i * 0.1, t);
            gain.gain.exponentialRampToValueAtTime(0.001, t + 0.4);
            osc.connect(gain);
            gain.connect(masterGain);
            osc.start(t);
            osc.stop(t + 0.4);
        });
    }

    function playMiss() {
        if (isMuted) return;
        const c = getCtx();
        resume();
        [330, 277].forEach((freq, i) => {
            const osc = c.createOscillator();
            const gain = c.createGain();
            const t = c.currentTime + i * 0.18;
            osc.type = 'sine';
            osc.frequency.setValueAtTime(freq, t);
            osc.frequency.exponentialRampToValueAtTime(freq * 0.7, t + 0.3);
            gain.gain.setValueAtTime(0.35, t);
            gain.gain.exponentialRampToValueAtTime(0.001, t + 0.35);
            osc.connect(gain);
            gain.connect(masterGain);
            osc.start(t);
            osc.stop(t + 0.35);
        });
    }

    function playGameOver() {
        if (isMuted) return;
        const c = getCtx();
        resume();
        [440, 392, 329.63, 261.63, 196].forEach((freq, i) => {
            const osc = c.createOscillator();
            const gain = c.createGain();
            const t = c.currentTime + i * 0.28;
            osc.type = 'triangle';
            osc.frequency.value = freq;
            gain.gain.setValueAtTime(0.3, t);
            gain.gain.exponentialRampToValueAtTime(0.001, t + 0.4);
            osc.connect(gain);
            gain.connect(masterGain);
            osc.start(t);
            osc.stop(t + 0.4);
        });
    }

    function playStart() {
        if (isMuted) return;
        const c = getCtx();
        resume();
        [196, 220, 261.63, 329.63, 392, 440].forEach((freq, i) => {
            const osc = c.createOscillator();
            const gain = c.createGain();
            const t = c.currentTime + i * 0.09;
            osc.type = 'triangle';
            osc.frequency.value = freq;
            gain.gain.setValueAtTime(0.28, t);
            gain.gain.exponentialRampToValueAtTime(0.001, t + 0.15);
            osc.connect(gain);
            gain.connect(masterGain);
            osc.start(t);
            osc.stop(t + 0.15);
        });
    }

    function start(chain = 0, beatCb = null) {
        if (isPlaying) return;
        resume();
        onBeatCallback = beatCb;
        bpm = bpmForChain(chain);
        isPlaying = true;
        currentStep = 0;
        arpeggioIndex = 0;
        nextStepTime = getCtx().currentTime + 0.05;
        scheduler();
    }

    function stop() {
        isPlaying = false;
        onBeatCallback = null;
        clearTimeout(schedulerTimer);
    }

    function updateBpm(chain) {
        bpm = bpmForChain(chain);
    }

    function toggleMute() {
        isMuted = !isMuted;
        if (masterGain) masterGain.gain.value = isMuted ? 0 : 0.5;
        return isMuted;
    }

    function judgeTiming() {
        const ms = msToNextBeat();
        const beat = beatIntervalMs();
        const dist = Math.min(ms, beat - ms);
        if (dist <= 120) return 'perfect';
        if (dist <= 280) return 'good';
        if (dist <= beat * 0.45) return 'late';
        return 'miss';
    }

    // ── THIS IS THE RETURN OBJECT ─────────────────────────────────────────────────
    return {
        start, stop, updateBpm, toggleMute,
        playHit, playMiss, playGameOver, playStart,
        judgeTiming, beatIntervalMs, bpmForChain
    };

    // ── THIS IS THE FINAL CLOSING LINE ──────────────────────────────────────────
})();