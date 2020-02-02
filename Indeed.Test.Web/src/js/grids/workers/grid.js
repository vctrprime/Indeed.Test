import { createDataSource } from "./dataSource";

function createGrid() {
  createDataSource().then((dataSource) => {
    $("#grid-workers").kendoGrid({
      dataSource: dataSource,
      pageable: {
        refresh: true,
        pageSizes: true,
        buttonCount: 5
      },
      columns: [
        {
          field: "id",
          title: "Личный номер"
        },
        {
          field: "name",
          title: "Имя"
        },
        {
          field: "function",
          title: "Должность"
        },
        {
          field: "status",
          title: "Статус",
          template: "<div class='status' style='background-color: #: statusColor #'>#: status #</div>"
        }
      ]
    });
  });

}

export {
  createGrid
}