import $ from 'jquery';
import validate from 'jquery-validation'
window.jQuery = $; window.$ = $;
const signalR = require("@aspnet/signalr");


import "@progress/kendo-ui";
import "@progress/kendo-ui/js/kendo.aspnetmvc";
import "@progress/kendo-theme-default/dist/all.css"; 

import { createGrid as createRequestsGrid } from './grids/requests/grid';
import { createGrid as createWorkersGrid } from './grids/workers/grid';
import { default as getSettings } from './settings/get';


$(function () {
  createRequestsGrid();
  createWorkersGrid();
  getSettings();


  let connection = new signalR.HubConnectionBuilder().withUrl("/distributeHub").build();
  connection.start().catch(err => console.error(err.toString()));
  connection.on("SendMessageToClient",  () => {
    $('#grid-requests').data('kendoGrid').dataSource.read();
    if ($('#grid-workers').attr('auto-refresh') === 'true') {
      $('#grid-workers').data('kendoGrid').dataSource.read();
    }
      
  });

  $('#auto-refresh-workers-btn').off().on('click', function () {
    let autoRefresh = $('#grid-workers').attr('auto-refresh');
    autoRefresh = autoRefresh === 'false';
    $('#grid-workers').attr('auto-refresh', autoRefresh);
    if (autoRefresh) {
      $('#grid-workers').data('kendoGrid').dataSource.read();
      $(this).text('Остановить автобновление');
    }
    else
      $(this).text('Возобновить автобновление');
  })
})