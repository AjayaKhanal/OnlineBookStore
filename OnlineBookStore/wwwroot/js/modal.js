document.addEventListener("DOMContentLoaded", function () {
	var modal = document.getElementById('modal');
	var emodal = document.getElementById('e-modal');
	var openBtn = document.querySelector(".open-modal");
	var closeBtn = document.querySelector(".close");
	var cancelBtn = document.querySelector(".btn-cancel");
	var saveBtn = document.querySelector(".btn-save");

	// Open modal when Author button is clicked
	openBtn.addEventListener("click", function () {
		modal.style.display = "flex";
		emodal.style.display = "none";
		saveBtn.style.display = "flex";
		document.getElementsByClassName("input-field")[0].focus();
	});

	// Close modal when close (X) button is clicked
	closeBtn.addEventListener("click", function () {
		modal.style.display = "none";
		emodal.style.display = "none";
	});

	// Close modal when cancel button is clicked
	cancelBtn.addEventListener("click", function () {
		modal.style.display = "none";
		emodal.style.display = "none";
	});

	// Close modal if user clicks outside the modal content
	window.addEventListener("click", function (event) {
		if (event.target === modal || event.target === emodal) {
			modal.style.display = "none";
		}
	});
});
