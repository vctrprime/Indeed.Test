export default function ($form) {
  $.post('settings/update', $form.serialize(), (result) => { alert('Настройки успешно сохранены!'); });
}