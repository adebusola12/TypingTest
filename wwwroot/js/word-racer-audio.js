// ── Word Racer Audio Engine ───────────────────────────────────────────────
// Uses Web Audio API to generate all sounds programmatically
// No external audio files needed

const WordRacerAudio = (function () {

    let ctx = null;
    let muted = false;
    let engineNode = null;
    let engineGain = null;
    let engineRunning = false;

    // ── Lazy-init AudioContext ────────────────────────────────────────────
    function getCtx() {
        if (!ctx) {
            ctx = new (window.AudioContext || window.webkitAudioContext)();
        }
        if (ctx.state === 'suspended') {
            ctx.resume();
        }
        return ctx;
    }

    // ── Master volume check ───────────────────────────────────────────────
    function canPlay() {
        return !muted;
    }

    // ── Utility: create gain node ─────────────────────────────────────────
    function makeGain(value) {
        const g = getCtx().createGain();
        g.gain.value = value;
        g.connect(getCtx().destination);
        return g;
    }

    // ── Utility: play a simple tone ───────────────────────────────────────
    function playTone(freq, type, duration, gainVal, startTime, endGain) {
        const c = getCtx();
        const osc = c.createOscillator();
        const gain = c.createGain();

        osc.type = type || 'sine';
        osc.frequency.setValueAtTime(freq, c.currentTime + (startTime || 0));

        gain.gain.setValueAtTime(gainVal || 0.3, c.currentTime + (startTime || 0));
        gain.gain.exponentialRampToValueAtTime(
            endGain || 0.001,
            c.currentTime + (startTime || 0) + duration
        );

        osc.connect(gain);
        gain.connect(c.destination);
        osc.start(c.currentTime + (startTime || 0));
        osc.stop(c.currentTime + (startTime || 0) + duration);
    }

    // ── Utility: noise burst (for explosions/crashes) ─────────────────────
    function playNoise(duration, gainVal, filterFreq) {
        const c = getCtx();
        const bufferSize = c.sampleRate * duration;
        const buffer = c.createBuffer(1, bufferSize, c.sampleRate);
        const data = buffer.getChannelData(0);

        for (let i = 0; i < bufferSize; i++) {
            data[i] = Math.random() * 2 - 1;
        }

        const source = c.createBufferSource();
        source.buffer = buffer;

        const filter = c.createBiquadFilter();
        filter.type = 'bandpass';
        filter.frequency.value = filterFreq || 400;
        filter.Q.value = 0.8;

        const gain = c.createGain();
        gain.gain.setValueAtTime(gainVal || 0.4, c.currentTime);
        gain.gain.exponentialRampToValueAtTime(0.001, c.currentTime + duration);

        source.connect(filter);
        filter.connect(gain);
        gain.connect(c.destination);
        source.start();
        source.stop(c.currentTime + duration);
    }

    // ── Engine Hum (background ambience while game runs) ─────────────────
    function startEngine(wave) {
        if (!canPlay()) return;
        stopEngine();

        const c = getCtx();
        const baseFreq = 60 + (wave * 8); // gets higher each wave

        engineGain = c.createGain();
        engineGain.gain.value = 0.06;
        engineGain.connect(c.destination);

        // Low rumble oscillator
        engineNode = c.createOscillator();
        engineNode.type = 'sawtooth';
        engineNode.frequency.value = baseFreq;
        engineNode.connect(engineGain);
        engineNode.start();

        // Add subtle pulse using LFO
        const lfo = c.createOscillator();
        const lfoGain = c.createGain();
        lfo.frequency.value = 6 + wave;
        lfoGain.gain.value = 8;
        lfo.connect(lfoGain);
        lfoGain.connect(engineNode.frequency);
        lfo.start();

        engineRunning = true;
    }

    function stopEngine() {
        try {
            if (engineNode) { engineNode.stop(); engineNode = null; }
            if (engineGain) { engineGain.disconnect(); engineGain = null; }
        } catch (e) { /* ignore */ }
        engineRunning = false;
    }

    function updateEngineWave(wave) {
        if (!engineRunning || !canPlay()) return;
        startEngine(wave); // restart with higher pitch
    }

    // ── Game Start ────────────────────────────────────────────────────────
    // Rising engine rev + countdown beeps
    function playStart() {
        if (!canPlay()) return;
        const c = getCtx();

        // Three countdown beeps
        [0, 0.35, 0.70].forEach((t, i) => {
            playTone(440 + (i * 110), 'square', 0.18, 0.2, t, 0.001);
        });

        // Final GO! sound — high pitch burst
        playTone(880, 'square', 0.25, 0.35, 1.05, 0.001);
        playTone(1100, 'sine', 0.3, 0.25, 1.05, 0.001);

        // Engine rev up sweep
        const osc = c.createOscillator();
        const gain = c.createGain();
        osc.type = 'sawtooth';
        osc.frequency.setValueAtTime(80, c.currentTime + 1.05);
        osc.frequency.linearRampToValueAtTime(200, c.currentTime + 1.6);
        gain.gain.setValueAtTime(0.15, c.currentTime + 1.05);
        gain.gain.exponentialRampToValueAtTime(0.001, c.currentTime + 1.65);
        osc.connect(gain);
        gain.connect(c.destination);
        osc.start(c.currentTime + 1.05);
        osc.stop(c.currentTime + 1.65);
    }

    // ── Car Destroyed (word typed correctly) ──────────────────────────────
    // Satisfying explosion pop + rising tone
    function playDestroy() {
        if (!canPlay()) return;

        // Explosion noise burst
        playNoise(0.18, 0.5, 600);

        // Rising success tone
        playTone(300, 'sine', 0.08, 0.25, 0, 0.001);
        playTone(500, 'sine', 0.08, 0.25, 0.06, 0.001);
        playTone(750, 'sine', 0.1, 0.3, 0.12, 0.001);
    }

    // ── Combo Sounds ──────────────────────────────────────────────────────
    // Higher combo = more impressive sound
    function playCombo(level) {
        if (!canPlay()) return;

        if (level === 3) {
            // x3 — simple double chime
            playTone(660, 'sine', 0.12, 0.25, 0, 0.001);
            playTone(880, 'sine', 0.14, 0.3, 0.1, 0.001);
        }
        else if (level === 5) {
            // x5 — rising triple chime
            playTone(550, 'sine', 0.1, 0.2, 0, 0.001);
            playTone(770, 'sine', 0.12, 0.25, 0.08, 0.001);
            playTone(1100, 'sine', 0.15, 0.3, 0.16, 0.001);
        }
        else if (level >= 8) {
            // x8 UNSTOPPABLE — dramatic fanfare
            [0, 0.07, 0.14, 0.21, 0.28].forEach((t, i) => {
                const freqs = [440, 550, 660, 770, 880];
                playTone(freqs[i], 'sine', 0.2, 0.35, t, 0.001);
            });
            playNoise(0.15, 0.2, 800);
        }
    }

    // ── Life Lost (car crossed finish line) ───────────────────────────────
    // Crash sound + descending tone
    function playMiss() {
        if (!canPlay()) return;

        // Screech noise
        playNoise(0.3, 0.6, 300);

        // Descending sad tone
        const c = getCtx();
        const osc = c.createOscillator();
        const g = c.createGain();
        osc.type = 'sawtooth';
        osc.frequency.setValueAtTime(400, c.currentTime);
        osc.frequency.exponentialRampToValueAtTime(100, c.currentTime + 0.4);
        g.gain.setValueAtTime(0.3, c.currentTime);
        g.gain.exponentialRampToValueAtTime(0.001, c.currentTime + 0.4);
        osc.connect(g);
        g.connect(c.destination);
        osc.start();
        osc.stop(c.currentTime + 0.4);
    }

    // ── Wave Advance ──────────────────────────────────────────────────────
    // Dramatic ascending fanfare
    function playWave(waveNum) {
        if (!canPlay()) return;

        const baseFreq = 220 + (waveNum * 20);

        // Ascending arpeggio
        [0, 0.1, 0.2, 0.3].forEach((t, i) => {
            playTone(
                baseFreq * Math.pow(1.25, i),
                'square',
                0.18,
                0.2,
                t,
                0.001
            );
        });

        // Final flourish
        playTone(baseFreq * 2, 'sine', 0.35, 0.3, 0.42, 0.001);
        playNoise(0.1, 0.15, 1000);

        // Update engine hum pitch
        updateEngineWave(waveNum);
    }

    // ── Power-Up Earned ───────────────────────────────────────────────────
    // Sparkly pickup sound
    function playPowerupEarned() {
        if (!canPlay()) return;

        const freqs = [523, 659, 784, 1047];
        freqs.forEach((f, i) => {
            playTone(f, 'sine', 0.15, 0.2, i * 0.06, 0.001);
        });
    }

    // ── Power-Up Activated ────────────────────────────────────────────────
    function playPowerupActivate(type) {
        if (!canPlay()) return;

        if (type && type.includes('FREEZE')) {
            // Ice crystal sound — descending high tones
            [1200, 1000, 800, 600].forEach((f, i) => {
                playTone(f, 'sine', 0.15, 0.2, i * 0.05, 0.001);
            });
        }
        else if (type && type.includes('SLOW')) {
            // Slow-mo whoosh
            const c = getCtx();
            const osc = c.createOscillator();
            const g = c.createGain();
            osc.type = 'sawtooth';
            osc.frequency.setValueAtTime(600, c.currentTime);
            osc.frequency.exponentialRampToValueAtTime(200, c.currentTime + 0.5);
            g.gain.setValueAtTime(0.2, c.currentTime);
            g.gain.exponentialRampToValueAtTime(0.001, c.currentTime + 0.5);
            osc.connect(g);
            g.connect(c.destination);
            osc.start();
            osc.stop(c.currentTime + 0.5);
        }
        else if (type && type.includes('BOMB')) {
            // Big explosion
            playNoise(0.4, 0.7, 200);
            playTone(100, 'sawtooth', 0.3, 0.4, 0, 0.001);
        }
        else if (type && type.includes('HEAL')) {
            // Warm healing chime
            [440, 550, 660, 880].forEach((f, i) => {
                playTone(f, 'sine', 0.2, 0.25, i * 0.08, 0.001);
            });
        }
    }

    // ── Game Over ─────────────────────────────────────────────────────────
    // Engine dying + sad descending tones
    function playGameOver() {
        if (!canPlay()) return;
        stopEngine();

        // Engine dying
        const c = getCtx();
        const osc = c.createOscillator();
        const g = c.createGain();
        osc.type = 'sawtooth';
        osc.frequency.setValueAtTime(150, c.currentTime);
        osc.frequency.exponentialRampToValueAtTime(30, c.currentTime + 1.2);
        g.gain.setValueAtTime(0.3, c.currentTime);
        g.gain.exponentialRampToValueAtTime(0.001, c.currentTime + 1.2);
        osc.connect(g);
        g.connect(c.destination);
        osc.start();
        osc.stop(c.currentTime + 1.2);

        // Sad descending tones
        [400, 300, 200, 150].forEach((f, i) => {
            playTone(f, 'sine', 0.25, 0.2, 0.3 + (i * 0.15), 0.001);
        });

        // Crash noise
        playNoise(0.5, 0.4, 250);
    }

    // ── Wrong Input ───────────────────────────────────────────────────────
    function playWrong() {
        if (!canPlay()) return;
        playTone(150, 'square', 0.12, 0.2, 0, 0.001);
    }

    // ── Mute Toggle ───────────────────────────────────────────────────────
    function toggleMute() {
        muted = !muted;
        if (muted) {
            stopEngine();
        }
        return muted;
    }

    function isMuted() { return muted; }

    // ── Stop All ──────────────────────────────────────────────────────────
    function stop() {
        stopEngine();
    }

    // ── Public API ────────────────────────────────────────────────────────
    return {
        playStart,
        playDestroy,
        playMiss,
        playWave,
        playCombo,
        playPowerupEarned,
        playPowerupActivate,
        playGameOver,
        playWrong,
        startEngine,
        stopEngine,
        updateEngineWave,
        toggleMute,
        isMuted,
        stop
    };

})();