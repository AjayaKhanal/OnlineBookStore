document.addEventListener("DOMContentLoaded", function () {
	var modal = document.getElementById('modal');
	var openBtn = document.querySelector(".open-modal");
	var closeBtn = document.querySelector(".close");
	var cancelBtn = document.querySelector(".btn-cancel");

	// Open modal when Add Author button is clicked
	openBtn.addEventListener("click", function () {
		modal.style.display = "flex";
		document.getElementsByClassName("input-field")[0].focus();
	});

	// Close modal when close (X) button is clicked
	closeBtn.addEventListener("click", function () {
		modal.style.display = "none";
	});

	// Close modal when cancel button is clicked
	cancelBtn.addEventListener("click", function () {
		modal.style.display = "none";
	});

	// Close modal if user clicks outside the modal content
	window.addEventListener("click", function (event) {
		if (event.target === modal) {
			modal.style.display = "none";
		}
	});
});