export default function ($form) {
  const data = $form.serialize();
  if ($form.valid()) {
    $.post('settings/update', data, (result) => { alert('Настройки успешно сохранены!'); })
      .fail(function (result) {
        alert(result.responseJSON.value.result);
      });
  }
  else {
    alert("Настройки не сохранены! Форма заполнена некорректно: диапазон значений от 10 до 100, нижняя граница не может быть больше верхней, границы не могут быть равны!");
  }
  
}
