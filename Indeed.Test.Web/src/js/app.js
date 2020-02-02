import $ from 'jquery';
window.jQuery = $; window.$ = $;
const signalR = require("@aspnet/signalr");


import "@progress/kendo-ui";
import "@progress/kendo-ui/js/kendo.aspnetmvc";
import "@progress/kendo-theme-default/dist/all.css"; 

import { template } from './templates/main';
import { createGrid as createRequestsGrid } from './grids/requests/grid';
import { createGrid as createWorkersGrid } from './grids/workers/grid';
import { default as getSettings } from './settings/get';


$(function () {
  $('body').append(template);
  createRequestsGrid();
  createWorkersGrid();
  getSettings();


  let connection = new signalR.HubConnectionBuilder().withUrl("/distributeHub").build();
  connection.start().catch(err => console.error(err.toString()));
  connection.on("SendMessageToClient",  () => {
    $('#grid-requests').data('kendoGrid').dataSource.read();
    $('#grid-workers').data('kendoGrid').dataSource.read();
  });
})