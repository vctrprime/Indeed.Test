import { createDataSource } from "./dataSource";

function createGrid() {
  createDataSource().then((dataSource) => {
    $("#grid-requests").kendoGrid({
      dataSource: dataSource,
      pageable: {
        refresh: true,
        pageSizes: true,
        buttonCount: 5
      },
      columns: [
        {
          field: "id",
          title: "Запрос",
          width: 80
        },
        {
          field: "watingTime",
          title: "Ожидание, сек",
          template: "#: executor == null ? '' : watingTime #"
        },
        {
          field: "executedTime",
          title: "Выполнение, сек",
          template: "#: isComplete == false ? '' : executedTime #"
        },
        {
          field: "summaryTime",
          title: "Общее время, сек",
          template: "#: isComplete == false ? '' : summaryTime #"
        },
        {
          field: "executor",
          title: "Исполнитель"
        },
        {
          field: "status",
          title: "Статус",
          width: 120,
          template: "<div class='status' style='background-color: #: statusColor #'>#: status #</div>"
        }
      ]
    });
  });

}

export {
  createGrid
}