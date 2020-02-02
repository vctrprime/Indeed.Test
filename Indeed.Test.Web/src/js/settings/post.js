export default function ($form) {
  $.post('settings/update', $form.serialize(), (result) => { alert('Настройки успешно сохранены!'); })
    .fail(function (result) {
      alert("Настройки не сохранены! Форма заполнена некорректно: диапазон значений от 5 до 100, нижняя граница не может быть больше верхней, границы не могут быть равны!")
      const json = result.responseJSON.value.result;
      Object.keys(json).forEach(function (k) {
        $(`#${k}`).val(json[k]);
      });
   });
}