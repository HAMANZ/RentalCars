// FleetERP Admin Dashboard JavaScript

document.addEventListener('DOMContentLoaded', function () {
    // Initialize active menu highlighting
    initActiveMenu();

    // Initialize sidebar toggle for mobile
    initSidebarToggle();

    // Initialize notification panel
    initNotificationPanel();

    // Initialize collapse icon rotation
    initCollapseIconRotation();
});

function initActiveMenu() {
    const activeLink = document.querySelector('.nav-link.active');

    if (activeLink) {
        const submenu = activeLink.closest('.collapse');
        if (submenu) {
            // Animate opening using Bootstrap's Collapse API
            const bsCollapse = new bootstrap.Collapse(submenu, {
                toggle: false
            });
            bsCollapse.show();

            // Expand the toggle button
            const parentToggle = document.querySelector('[href="#' + submenu.id + '"]');
            if (parentToggle) {
                parentToggle.setAttribute('aria-expanded', 'true');
            }

            // Add class to parent nav-item
            const parentNavItem = submenu.closest('.nav-item');
            if (parentNavItem) {
                parentNavItem.classList.add('parent-active');
            }

            // Add class to child nav-item
            const childNavItem = activeLink.closest('.nav-item');
            if (childNavItem) {
                childNavItem.classList.add('child-active');
            }
        }
    }

    // Collapse the current submenu when any child link is clicked
    document.querySelectorAll('.sidebar .collapse').forEach(function (submenu) {
        submenu.querySelectorAll('a.nav-link').forEach(function (link) {
            link.addEventListener('click', function () {
                const instance = bootstrap.Collapse.getOrCreateInstance(submenu);
                instance.hide();
            });
        });
    });
}

function initSidebarToggle() {
    const sidebarToggle = document.getElementById('sidebarToggle');
    const sidebar = document.getElementById('sidebarContainer');

    if (sidebarToggle && sidebar) {
        sidebarToggle.addEventListener('click', (e) => {
            e.stopPropagation();
            sidebar.classList.toggle('active');
        });

        document.addEventListener('click', (e) => {
            if (!sidebar.contains(e.target) && !sidebarToggle.contains(e.target)) {
                sidebar.classList.remove('active');
            }
        });
    }
}

function initNotificationPanel() {
    const toggleBtn = document.getElementById('notificationToggle');
    const panel = document.getElementById('notificationPanel');
    const closeBtn = document.getElementById('closeNotification');

    if (toggleBtn && panel) {
        toggleBtn.addEventListener('click', (e) => {
            e.stopPropagation();
            panel.style.display =
                panel.style.display === 'none' || panel.style.display === ''
                    ? 'block'
                    : 'none';
        });

        document.addEventListener('click', function (e) {
            if (!toggleBtn.contains(e.target) && !panel.contains(e.target)) {
                panel.style.display = 'none';
            }
        });
    }

    if (closeBtn) {
        closeBtn.addEventListener('click', () => {
            panel.style.display = 'none';
        });
    }
}

function initCollapseIconRotation() {
    const toggleLinks = document.querySelectorAll('.nav-link[data-bs-toggle="collapse"]');

    toggleLinks.forEach(link => {
        const collapseId = link.getAttribute('href').substring(1);
        const collapseElement = document.getElementById(collapseId);
        const icon = link.querySelector('.toggle-icon');

        if (collapseElement && icon) {
            collapseElement.addEventListener("show.bs.collapse", function () {
                icon.style.transform = "rotate(180deg)";
                icon.style.transition = "transform 0.3s ease";
            });

            collapseElement.addEventListener("hide.bs.collapse", function () {
                icon.style.transform = "rotate(0deg)";
                icon.style.transition = "transform 0.3s ease";
            });
        }
    });
}

// Utility function to show toast notifications
function showToast(message, type = 'success') {
    const toastContainer = document.getElementById('toastContainer');
    if (!toastContainer) {
        const container = document.createElement('div');
        container.id = 'toastContainer';
        container.className = 'toast-container position-fixed bottom-0 end-0 p-3';
        container.style.zIndex = '1100';
        document.body.appendChild(container);
    }

    const toastId = 'toast-' + Date.now();
    const bgClass = type === 'success' ? 'bg-success' : type === 'error' ? 'bg-danger' : 'bg-warning';

    const toastHtml = `
        <div id="${toastId}" class="toast align-items-center text-white ${bgClass} border-0" role="alert">
            <div class="d-flex">
                <div class="toast-body">${message}</div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>
            </div>
        </div>
    `;

    document.getElementById('toastContainer').insertAdjacentHTML('beforeend', toastHtml);
    const toast = new bootstrap.Toast(document.getElementById(toastId));
    toast.show();
}

// Utility function for delete confirmations
function confirmDelete(message, callback) {
    if (confirm(message || 'Are you sure you want to delete this item?')) {
        callback();
    }
}
