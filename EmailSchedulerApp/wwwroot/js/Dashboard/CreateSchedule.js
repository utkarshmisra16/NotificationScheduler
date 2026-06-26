let currentStep = 1;

document.addEventListener("DOMContentLoaded", () => {
    initializeForm();
    showStep(currentStep);
});

function initializeForm() {
    const today = new Date().toISOString().split('T')[0];

    const startDate = document.getElementById("sdate");
    const endDate = document.getElementById("edate");

    if (startDate) {
        startDate.min = today;
        startDate.value = today;
    }

    if (endDate) {
        endDate.min = today;
    }
}

function showStep(step) {

    document.querySelectorAll(".step-panel")
        .forEach(x => x.style.display = "none");

    document.getElementById(`step${step}`)?.style.setProperty("display", "block");

    document.getElementById("prevBtn").style.display =
        step > 1 ? "inline-flex" : "none";

    document.getElementById("nextBtn").style.display =
        step === 4 ? "none" : "inline-flex";

    document.getElementById("submitBtn").style.display =
        step === 4 ? "inline-flex" : "none";
}

function nextStep() {

    if (!validateStep(currentStep))
        return;

    currentStep++;

    if (currentStep > 4)
        currentStep = 4;

    showStep(currentStep);

    if (currentStep === 4)
        syncReview();
}

function previousStep() {

    currentStep--;

    if (currentStep < 1)
        currentStep = 1;

    showStep(currentStep);
}

let selectedFrequency = "Daily";

function setFreq(element, frequency) {

    document
        .querySelectorAll(".freq-card")
        .forEach(x => x.classList.remove("selected"));

    element.classList.add("selected");

    selectedFrequency = frequency;

    document.getElementById("weekly-options").style.display =
        frequency === "Weekly" ? "block" : "none";

    document.getElementById("custom-options").style.display =
        frequency === "Custom" ? "block" : "none";
}

function validateStep(step) {

    if (step === 1) {

        const scheduleName =
            document.getElementById("sname").value.trim();

        if (!scheduleName) {
            alert("Schedule name is required.");
            return false;
        }
    }

    if (step === 2) {

        const startDate =
            document.getElementById("sdate").value;

        const startTime =
            document.getElementById("stime").value;

        if (!startDate || !startTime) {
            alert("Start date and time are required.");
            return false;
        }
    }

    if (step === 3) {

        const emails =
            document.getElementById("custom-emails").value.trim();

        if (!emails) {
            alert("At least one recipient email is required.");
            return false;
        }
    }

    return true;
}

function syncReview() {

    const emails = document
        .getElementById("custom-emails")
        .value
        .split(/[\n,]/)
        .map(x => x.trim())
        .filter(x => x);

    document.getElementById("rv-name").textContent =
        document.getElementById("sname").value || "Not set";

    document.getElementById("rv-channel").textContent =
        document.querySelector('input[name="channel"]:checked')?.value || "Email";

    document.getElementById("rv-tmpl").textContent =
        document.getElementById("stemplate").selectedOptions[0]?.text || "Not set";

    document.getElementById("rv-desc").textContent =
        document.getElementById("sdesc").value || "Not set";

    document.getElementById("rv-datetime").textContent =
        `${document.getElementById("sdate").value || "-"} ${document.getElementById("stime").value || "-"}`;

    document.getElementById("rv-tz").textContent =
        document.getElementById("stimezone").value;

    document.getElementById("rv-freq").textContent =
        selectedFrequency;

    document.getElementById("rv-edate").textContent =
        document.getElementById("edate").value || "No Limit";

    document.getElementById("rv-recip-count").textContent =
        emails.length;

    document.getElementById("total-recipients").textContent =
        emails.length;
}

function collectFormData() {

    return {

        name: document.getElementById("sname").value,

        templateId: parseInt(
            document.getElementById("stemplate").value
        ),

        description:
            document.getElementById("sdesc").value,

        startDate:
            document.getElementById("sdate").value,

        startTime:
            document.getElementById("stime").value,

        timezone:
            document.getElementById("stimezone").value,

        frequency:
            selectedFrequency,

        weekDays:
            Array.from(
                document.querySelectorAll('input[name="day"]:checked')
            ).map(x => x.value),

        cronExpression:
            document.getElementById("cron-input").value,

        endDate:
            document.getElementById("edate").value || null,

        recipientEmails:
            document.getElementById("custom-emails")
                .value
                .split(/[\n,]/)
                .map(x => x.trim())
                .filter(x => x),

        tags:
            document.getElementById("rv-tags").value,

        priority: document.getElementById("rv-priority").value.charAt(0).toUpperCase() + document.getElementById("rv-priority").value.slice(1)
    };
}

async function submitSchedule() {

    const data = collectFormData();

    const response = await fetch(
        "/Schedule/Schedule/CreateSchedule",
        {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        });

    const result = await response.json();

    if (result.success) {
        window.location.href = "/Dashboard";
    }
    else {
        alert(result.message);
    }
}

function goStep(stepNumber) {
    if (stepNumber >= 1 && stepNumber <= 4) {
        currentStep = stepNumber;
        showStep(currentStep);
    }
}