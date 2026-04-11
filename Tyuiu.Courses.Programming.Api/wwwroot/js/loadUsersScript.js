document.getElementById('fileInput').addEventListener('change', function (event) {
	const file = event.target.files[0];
	if (file) {
		const importModal = new bootstrap.Modal(document.getElementById('importModal'));
		const fileReader = new FileReader();

		fileReader.onload = function (e) {
			const fileContent = e.target.result;
			let parsedData = [];

			if (file.type === 'application/json') {
				parsedData = JSON.parse(fileContent);
			} else if (file.type === 'text/csv') {
				parsedData = parseCSV(fileContent);
			} else if (file.type === 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' ||
				file.type === 'application/vnd.ms-excel') {
				const workbook = XLSX.read(fileContent, { type: 'binary' });
				const firstSheetName = workbook.SheetNames[0];
				const worksheet = workbook.Sheets[firstSheetName];
				parsedData = XLSX.utils.sheet_to_json(worksheet);
			}

			if (parsedData.length > 0) {
				populateTable(parsedData);
			} else {
				console.error("No data to display.");
			}

			// Очистка значения input
			event.target.value = '';
		};

		if (file.type === 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' ||
			file.type === 'application/vnd.ms-excel') {
			fileReader.readAsBinaryString(file);
		} else {
			fileReader.readAsText(file);
		}
	}
});

function parseCSV(data) {
	const lines = data.trim().split("\n");
	const headers = lines[0].split(";").map(header => header.trim());

	const result = lines.slice(1).map(line => {
		const obj = {};
		const currentLine = line.split(";").map(value => value.trim());
		headers.forEach((header, index) => {
			obj[header] = currentLine[index];
		});
		return obj;
	});

	return result;
}

function populateTable(data) {
	const tbody = document.querySelector('table tbody');
	tbody.innerHTML = ''; // Clear the existing rows

	data.forEach(item => {
		const row = document.createElement('tr');
		row.innerHTML = `
					<td class="ps-4">${item.Surname || ''}</th>
					<td>${item.Name || ''}</td>
					<td>${item.Patronymic || ''}</td>
					<td>${item.Email || ''}</td>
					<td>
						<button type="button" class="btn btn-outline-primary shadow-none btn-sm" title="Редактировать">
							<i class="bi bi-pencil fs-5"></i>
						</button>
					</td>
				`;
		tbody.appendChild(row);
	});
}
document.addEventListener('DOMContentLoaded', function () {
	var importModal = document.getElementById('importModal');
	var closeButton = document.getElementById('closeModalButton');

	importModal.addEventListener('hidden.bs.modal', function () {
		location.reload();
	});

	closeButton.addEventListener('click', function () {
		location.reload();
	});
});