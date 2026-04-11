tinymce.init({
	selector: 'textarea#tiny',
	license_key: 'gpl',
	plugins: 'autosave link autolink charmap codesample image lists media lists advlist quickbars searchreplace table',
	toolbar: 'restoredraft undo redo | styles fontfamily fontsize bold italic underline forecolor backcolor | alignleft aligncenter alignright alignjustify lineheight | numlist bullist | link image media table | codesample blockquote charmap subscript superscript removeformat cut searchreplace',
	toolbar_mode: 'sliding',
	menubar: false,
	quickbars_selection_toolbar: 'bold italic underline forecolor backcolor removeformat | cut codesample blockquote | quicklink',
	readonly: window.tinyReadOnly === true
});

function updateTiny() {
	$('#tiny').val(tinymce.get('tiny').getContent());
}