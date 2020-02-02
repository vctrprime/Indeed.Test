import post from "./post";

export default function(){
  $.get('settings/get', (result) => {
    Object.keys(result).forEach(function (k) {
      $(`#${k}`).val(result[k]);
    });
    $('#settings-form-container button').off().on('click', function () {
      const $form = $(this).closest('form');
      post($form);
    })
  });
}