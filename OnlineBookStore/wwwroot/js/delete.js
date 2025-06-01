$(document).ready(function () {
    const $deleteModal = $('#deleteModal');
    const $authorIdInput = $('#deleteId');
    const $authorNameSpan = $('#deleteName');

    $('.open-delete-modal').on('click', function () {
        const authorId = $(this).data('author-id');
        const authorName = $(this).data('author-name');

        $authorIdInput.val(authorId);
        $authorNameSpan.text(authorName);
    });
});