import { createDataSource } from "./dataSource";

function createGrid() {
  const holder = "#grid-workers";
  const positions = [
    {
      "value": "Operator",
      "text": "Operator"
    },
    {
      "value": "Manager",
      "text": "Manager"
    },
    {
      "value": "Director",
      "text": "Director"
    }
  ]
  createDataSource(holder).then((dataSource) => {
    $(holder).kendoGrid({
      dataSource: dataSource,
      pageable: {
        refresh: true,
        pageSizes: true,
        buttonCount: 5
      },
      toolbar: ["create"],
      columns: [
        { field: "id", title: "Личный номер" },
        { field: "name", title: "Имя" },
        { field: "position", title: "Должность", values: positions },
        { field: "status", title: "Статус", template: "<div class='status' style='background-color: #: statusColor #'>#: status #</div>"},
        { command: ["edit", "destroy"], title: "&nbsp;", width: 190 }
      ],
      editable: "inline"
    });
  });

}

export {
  createGrid
}