document.addEventListener("DOMContentLoaded", function () {
	// Delete Confirmation Modal
	//var delOpenBtn = document.querySelector(".open-delete-modal");
	var delModal = document.getElementById("deleteModal");
	var delBtn = document.querySelectorAll(".del-butn");
	var delCloseBtn = document.querySelector(".dclose");
	var delCancelBtn = document.querySelector(".btn-dcancel");
	const idInput = document.getElementById("deleteId");
	const nameSpan = document.getElementById("deleteName");

	delBtn.forEach(btn => {
		btn.addEventListener("click", function () {
			const id = btn.getAttribute("data-id");
			const name = btn.getAttribute("data-name");

			if (idInput) idInput.value = id;
			if (nameSpan) nameSpan.textContent = name;

			//if (nameSpan) nameSpan.style.display = "flex";

			delModal.style.display = "flex";
		});
	});

	delCloseBtn?.addEventListener("click", function () {
		console.log("click");
		delModal.style.display = "none";
	});

	delCancelBtn?.addEventListener("click", function () {
		console.log("click");
		delModal.style.display = "none";
	});

	window.addEventListener("click", function (event) {
		if (event.target === delModal) {
			delModal.style.display = "none";
		}
	});
});