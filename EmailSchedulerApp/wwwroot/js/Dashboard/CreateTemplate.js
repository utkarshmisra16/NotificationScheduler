let quill;
let selectedFiles = [];
let dropZone, fileInput, fileList;

$(document).ready(function () {
    $("#createBtn").click(saveTemplate);
    quill = new Quill('#quill-editor', {
        theme: 'snow',
        placeholder: 'Write your email content here...',
        modules: {
            toolbar: [
                [{ 'header': [1, 2, 3, false] }],
                ['bold', 'italic', 'underline', 'strike'],
                [{ 'color': [] }, { 'background': [] }],
                [{ 'list': 'ordered' }, { 'list': 'bullet' }],
                [{ 'align': [] }],
                ['link', 'blockquote', 'code-block'],
                ['clean']
            ]
        }
    });

    dropZone = document.getElementById('dropZone');
    fileInput = document.getElementById('attachmentInput');
    fileList = document.getElementById('fileList');

    dropZone.addEventListener('dragover', e => { e.preventDefault(); dropZone.classList.add('dragover'); });
    dropZone.addEventListener('dragleave', () => dropZone.classList.remove('dragover'));
    dropZone.addEventListener('drop', e => {
        e.preventDefault();
        dropZone.classList.remove('dragover');
        addFiles(e.dataTransfer.files);
    });

    fileInput.addEventListener('change', () => addFiles(fileInput.files));
});

function addFiles(fileArr) {
    const MAX = 10 * 1024 * 1024;
    Array.from(fileArr).forEach(f => {
        if (f.size > MAX) { alert(f.name + ' exceeds 10MB limit.'); return; }
        if (selectedFiles.find(x => x.name === f.name && x.size === f.size)) return;
        selectedFiles.push(f);
    });
    renderFileList();
    fileInput.value = '';
}

function getIconClass(name) {
    const ext = name.split('.').pop().toLowerCase();
    if (['pdf'].includes(ext)) return 'pdf';
    if (['doc', 'docx', 'txt'].includes(ext)) return 'doc';
    if (['png', 'jpg', 'jpeg', 'gif', 'webp'].includes(ext)) return 'img';
    if (['xls', 'xlsx', 'csv'].includes(ext)) return 'xls';
    return 'other';
}

function getIconName(cls) {
    const map = { pdf: 'bi-file-earmark-pdf', doc: 'bi-file-earmark-text', img: 'bi-file-earmark-image', xls: 'bi-file-earmark-spreadsheet', other: 'bi-file-earmark' };
    return map[cls] || 'bi-file-earmark';
}

function formatSize(bytes) {
    if (bytes < 1024) return bytes + ' B';
    if (bytes < 1024 * 1024) return (bytes / 1024).toFixed(1) + ' KB';
    return (bytes / (1024 * 1024)).toFixed(1) + ' MB';
}

function renderFileList() {
    fileList.innerHTML = '';
    selectedFiles.forEach((f, i) => {
        const cls = getIconClass(f.name);
        const icon = getIconName(cls);
        const div = document.createElement('div');
        div.className = 'file-item';
        div.innerHTML =
            '<div class="fi-icon ' + cls + '"><i class="bi ' + icon + '"></i></div>' +
            '<span class="fi-name" title="' + f.name + '">' + f.name + '</span>' +
            '<span class="fi-size">' + formatSize(f.size) + '</span>' +
            '<button class="fi-remove" onclick="removeFile(' + i + ')" title="Remove"><i class="bi bi-x-lg"></i></button>';
        fileList.appendChild(div);
    });
}

function removeFile(index) {
    selectedFiles.splice(index, 1);
    renderFileList();
}

function submitForm() {
    document.getElementById('bodyContent').value = quill.root.innerHTML;
}

function validateFields() {
    let isValid = true;
    const templateName = $("#templateName").val().trim();
    const subject = $("#subject").val().trim();
    const bodyContent = quill.root.innerHTML.trim();

    if (!templateName) {
        $("#templateNameError").text("Template Name is required.");
        isValid = false;
    } else {
        $("#templateNameError").text("");
    }

    if (!subject) {
        $("#subjectError").text("Subject is required.");
        isValid = false;
    } else {
        $("#subjectError").text("");
    }

    if (!bodyContent || bodyContent === "<p><br></p>") {
        $("#bodyError").text("Email Body is required.");
        isValid = false;
    } else {
        $("#bodyError").text("");
    }

    return isValid;
}

function saveTemplate() {
    if (!validateFields()) return;
    var formData = new FormData();
    formData.append("TemplateName", $("#templateName").val());
    formData.append("Subject", $("#subject").val());
    formData.append("Body", quill.root.innerHTML);
    formData.append("IsActive", true);
    var files = $("#attachmentInput")[0].files;
    for (let i = 0; i < files.length; i++) {
        formData.append("Attachments", files[i]);
    }

    $.ajax({
        url: "/Template/Template/CreateTemplate",
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,
        success: function (response) {
            if (response.success) {
                console.log("SUCCESS");
                showSuccess(response.message);
                // Form Reset
                $("#templateName").val("");
                $("#subject").val("");
                quill.setContents([]);
                $("#attachmentInput").val("");

                // Top par scroll
                window.scrollTo({ top: 0, behavior: "smooth" });
            }
            else {
                showError(response.message);
            }
        },

        error: function (xhr, status, error) {
            showError("Something went wrong. Please try again.");
        }
    });
}