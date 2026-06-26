document.addEventListener('submit', function (event) {
    const form = event.target.closest('form');
    const confirmMessage = event.submitter?.dataset.confirm || form?.dataset.confirm;

    if (!confirmMessage) {
        return;
    }

    if (!confirm(confirmMessage)) {
        event.preventDefault();
    }
});