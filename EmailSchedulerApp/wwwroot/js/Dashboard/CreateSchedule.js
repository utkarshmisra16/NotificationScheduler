let currentStep = 2;
let selectedFreq = 'Daily';
let selectedRecips = [];

function goStep(n) {
    if (n < 1 || n > 4) return;

    document.getElementById('step' + currentStep).style.display = 'none';
    currentStep = n;
    document.getElementById('step' + n).style.display = 'block';

    document.querySelectorAll('.nav-item').forEach((el, i) => {
        el.classList.remove('active', 'done');
        const num = el.querySelector('.nav-num');
        if (i + 1 < n) {
            el.classList.add('done');
            num.innerHTML = '<i class="ti ti-check" style="font-size:10px"></i>';
        } else if (i + 1 === n) {
            el.classList.add('active');
            num.textContent = i + 1;
        } else {
            num.textContent = i + 1;
        }
    });

    document.getElementById('prevBtn').style.display = n > 1 ? 'inline-flex' : 'none';
    document.getElementById('cancelBtn').style.display = n > 1 ? 'none' : 'inline-flex';

    const nb = document.getElementById('nextBtn');
    if (n === 4) {
        nb.innerHTML = '<i class="ti ti-send" style="font-size:14px"></i> Create schedule';
        syncReview();
    } else {
        nb.innerHTML = 'Continue <i class="ti ti-arrow-right" style="font-size:14px"></i>';
    }
}

function nextStep() {
    if (currentStep < 4) goStep(currentStep + 1);
}

function setFreq(el, label) {
    document.querySelectorAll('.ftile').forEach(f => f.classList.remove('on'));
    el.classList.add('on');
    selectedFreq = label;
}

function toggleRecip(el, name) {
    el.classList.toggle('on');
    if (el.classList.contains('on')) {
        if (!selectedRecips.includes(name)) selectedRecips.push(name);
    } else {
        selectedRecips = selectedRecips.filter(r => r !== name);
    }
}

function syncReview() {
    const name = document.getElementById('sname').value;
    const tmpl = document.getElementById('stemplate').value;
    const date = document.getElementById('sdate').value;

    const setVal = (id, val, fallback) => {
        const el = document.getElementById(id);
        if (val) {
            el.textContent = val;
            el.classList.remove('empty');
        } else {
            el.textContent = fallback;
            el.classList.add('empty');
        }
    };

    setVal('rv-name', name, 'Not set');
    setVal('rv-tmpl', tmpl, 'Not set');
    setVal('rv-date', date, 'Not set');
    document.getElementById('rv-freq').textContent = selectedFreq;
    setVal('rv-recip', selectedRecips.join(', '), 'None selected');
}

document.getElementById('prevBtn').style.display = 'none';