// ====================================
// CREATE SCHEDULE - LOGIC & INTERACTIONS
// ====================================

let currentStep = 2; // Start at step 2
let selectedFrequency = 'Daily';
let selectedRecipient = 'department';
let formData = {};

// Initialize
document.addEventListener('DOMContentLoaded', function() {
  initializeForm();
  showStep(currentStep);
});

function initializeForm() {
  // Set minimum date to today
  const today = new Date().toISOString().split('T')[0];
  document.getElementById('sdate').min = today;
  document.getElementById('edate').min = today;
  document.getElementById('sdate').value = today;
}

// ====================================
// STEP NAVIGATION
// ====================================

function showStep(stepNumber) {
  // Hide all steps
  document.querySelectorAll('.step-panel').forEach(panel => {
    panel.style.display = 'none';
    panel.classList.remove('active');
  });

  // Show current step
  const currentPanel = document.getElementById(`step${stepNumber}`);
  if (currentPanel) {
    currentPanel.style.display = 'block';
    currentPanel.classList.add('active');
  }

  // Update progress indicators
  updateProgressIndicators(stepNumber);
  updateActionBar(stepNumber);
}

function updateProgressIndicators(stepNumber) {
  for (let i = 1; i <= 4; i++) {
    const indicator = document.getElementById(`step-indicator-${i}`);
    indicator.classList.remove('active', 'completed');

    if (i < stepNumber) {
      indicator.classList.add('completed');
    } else if (i === stepNumber) {
      indicator.classList.add('active');
    }
  }
}

function updateActionBar(stepNumber) {
  const prevBtn = document.getElementById('prevBtn');
  const nextBtn = document.getElementById('nextBtn');
  const submitBtn = document.getElementById('submitBtn');
  const draftBtn = document.getElementById('draftBtn');

  // Show/hide back button
  prevBtn.style.display = stepNumber > 1 ? 'inline-flex' : 'none';

  // Show/hide next and submit buttons
  if (stepNumber === 4) {
    nextBtn.style.display = 'none';
    submitBtn.style.display = 'inline-flex';
  } else {
    nextBtn.style.display = 'inline-flex';
    submitBtn.style.display = 'none';
  }
}

function goStep(stepNumber) {
  if (stepNumber >= 1 && stepNumber <= 4) {
    currentStep = stepNumber;
    showStep(currentStep);
  }
}

function nextStep() {
  if (currentStep < 4) {
    // Validate current step before moving
    if (validateStep(currentStep)) {
      currentStep++;
      showStep(currentStep);
      if (currentStep === 4) {
        syncReview();
      }
    }
  }
}

// ====================================
// FREQUENCY SELECTION
// ====================================

function setFreq(element, frequency) {
  // Remove selected class from all freq-cards
  document.querySelectorAll('.freq-card').forEach(card => {
    card.classList.remove('selected');
  });

  // Add selected class to clicked element
  element.classList.add('selected');
  selectedFrequency = frequency;

  // Show/hide conditional options
  const weeklyOptions = document.getElementById('weekly-options');
  const customOptions = document.getElementById('custom-options');

  weeklyOptions.style.display = frequency === 'Weekly' ? 'block' : 'none';
  customOptions.style.display = frequency === 'Custom' ? 'block' : 'none';

  syncReview();
}

// ====================================
// RECIPIENT SELECTION
// ====================================

function toggleRecip(element, recipientType) {
  // Remove selected class from all recip-cards
  document.querySelectorAll('.recip-card').forEach(card => {
    card.classList.remove('selected');
  });

  // Add selected class to clicked element
  element.classList.add('selected');
  selectedRecipient = recipientType;

  // Show/hide conditional sections
  const deptSelect = document.getElementById('department-select');
  const customList = document.getElementById('custom-email-list');

  deptSelect.style.display = recipientType === 'department' ? 'block' : 'none';
  customList.style.display = recipientType === 'custom-list' ? 'block' : 'none';

  updateRecipientCount();
  syncReview();
}

function updateRecipientCount() {
  let count = 0;

  if (selectedRecipient === 'all-users') {
    count = 245; // Example total users
  } else if (selectedRecipient === 'department') {
    const checkedDepts = document.querySelectorAll('input[name="dept"]:checked');
    checkedDepts.forEach(dept => {
      const label = dept.nextElementSibling.textContent;
      const match = label.match(/(\d+)/);
      if (match) count += parseInt(match[0]);
    });
  } else if (selectedRecipient === 'custom-list') {
    const emails = document.getElementById('custom-emails').value;
    const emailList = emails.split(/[,\n]/).filter(e => e.trim().length > 0);
    count = emailList.length;
  }

  document.getElementById('total-recipients').textContent = count;
}

// ====================================
// SYNC REVIEW SECTION
// ====================================

function syncReview() {
  // Basic Info
  const name = document.getElementById('sname').value || 'Not set';
  const channel = document.querySelector('input[name="channel"]:checked').value || 'Not set';
  const template = document.getElementById('stemplate').value || 'Not set';
  const desc = document.getElementById('sdesc').value || 'Not set';

  document.getElementById('rv-name').textContent = name;
  document.getElementById('rv-channel').textContent = channel;
  document.getElementById('rv-tmpl').textContent = template || 'Not set';
  document.getElementById('rv-desc').textContent = desc === '' ? 'Not set' : desc;

  // Schedule Info
  const startDate = document.getElementById('sdate').value || 'Not set';
  const startTime = document.getElementById('stime').value || '00:00';
  const timezone = document.getElementById('stimezone').value || 'UTC';
  const endDate = document.getElementById('edate').value || 'No limit';
  const maxRetries = document.getElementById('max-retries').value || '3';

  document.getElementById('rv-datetime').textContent = startDate + ' at ' + startTime;
  document.getElementById('rv-tz').textContent = timezone;
  document.getElementById('rv-freq').textContent = selectedFrequency;
  document.getElementById('rv-edate').textContent = endDate;
  document.getElementById('rv-retries').textContent = maxRetries;

  // Recipients Info
  const recipientLabels = {
    'all-users': 'All Users',
    'department': 'By Department',
    'role': 'By Role',
    'custom-list': 'Custom List'
  };

  document.getElementById('rv-recip-type').textContent = recipientLabels[selectedRecipient] || 'Not set';
  updateRecipientCount();
}

function updateMinEndDate() {
  const startDate = document.getElementById('sdate').value;
  document.getElementById('edate').min = startDate;
}

// ====================================
// VALIDATION
// ====================================

function validateStep(step) {
  switch (step) {
    case 1:
      const name = document.getElementById('sname').value.trim();
      if (!name) {
        showValidationError('Please enter a schedule name');
        return false;
      }
      return true;

    case 2:
      const date = document.getElementById('sdate').value;
      const time = document.getElementById('stime').value;
      if (!date || !time) {
        showValidationError('Please select both date and time');
        return false;
      }
      return true;

    case 3:
      if (selectedRecipient === 'custom-list') {
        const emails = document.getElementById('custom-emails').value.trim();
        if (!emails) {
          showValidationError('Please enter at least one email address');
          return false;
        }
      }
      return true;

    default:
      return true;
  }
}

function showValidationError(message) {
  alert('Validation Error: ' + message);
  // You can replace this with a better toast/notification system
}

// ====================================
// FORM ACTIONS
// ====================================

function cancelSchedule() {
  if (confirm('Are you sure you want to cancel? All unsaved changes will be lost.')) {
    window.location.href = '/dashboard';
  }
}

function saveDraft() {
  // Collect form data
  const draftData = collectFormData();
  // Save to localStorage or API
  localStorage.setItem('scheduleFormDraft', JSON.stringify(draftData));
  alert('Schedule saved as draft!');
}

function submitSchedule() {
  // Collect final form data
  const finalData = collectFormData();

  console.log('Submitting schedule:', finalData);

  // Send to API
  fetch('/api/schedules/create', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(finalData)
  })
    .then(response => response.json())
    .then(data => {
      if (data.success) {
        alert('Schedule created successfully!');
        window.location.href = '/dashboard/schedules';
      } else {
        alert('Error creating schedule: ' + data.message);
      }
    })
    .catch(error => {
      console.error('Error:', error);
      alert('An error occurred while creating the schedule.');
    });
}

function collectFormData() {
  const daysSelected = Array.from(document.querySelectorAll('input[name="day"]:checked')).map(d => d.value);
  const deptsSelected = Array.from(document.querySelectorAll('input[name="dept"]:checked')).map(d => d.value);

  const formData = {
    // Step 1
    scheduleName: document.getElementById('sname').value,
    channel: document.querySelector('input[name="channel"]:checked').value,
    template: document.getElementById('stemplate').value,
    description: document.getElementById('sdesc').value,

    // Step 2
    startDate: document.getElementById('sdate').value,
    startTime: document.getElementById('stime').value,
    timezone: document.getElementById('stimezone').value,
    frequency: selectedFrequency,
    days: daysSelected,
    cronExpression: document.getElementById('cron-input').value,
    endDate: document.getElementById('edate').value,
    maxRetries: parseInt(document.getElementById('max-retries').value),
    retryInterval: parseInt(document.getElementById('retry-interval').value),

    // Step 3
    recipientType: selectedRecipient,
    departments: deptsSelected,
    customEmails: document.getElementById('custom-emails').value,
    excludeEmails: document.getElementById('exclude-emails').value,

    // Step 4
    tags: document.getElementById('rv-tags').value,
    priority: document.getElementById('rv-priority').value
  };

  return formData;
}

// ====================================
// FILE UPLOAD HANDLER
// ====================================

document.getElementById('email-file')?.addEventListener('change', function(e) {
  const file = e.target.files[0];
  if (!file) return;

  const reader = new FileReader();
  reader.onload = function(event) {
    const content = event.target.result;
    const emails = content
      .split(/[\n,]/)
      .map(e => e.trim())
      .filter(e => e.length > 0 && e.includes('@'));

    document.getElementById('custom-emails').value = emails.join('\n');
    updateRecipientCount();
    syncReview();
  };
  reader.readAsText(file);
});

// ====================================
// HELPER FUNCTIONS
// ====================================

function formatDateTime(date, time) {
  return new Date(date + 'T' + time).toLocaleString();
}
