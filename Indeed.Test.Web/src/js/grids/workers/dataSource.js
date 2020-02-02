function createDataSource(holder) {
  return new Promise((resolve, reject) => {
    const crudServiceBaseUrl = "workers";
    const dataSource = new kendo.data.DataSource({
        transport: {
          read: {
            url: crudServiceBaseUrl,
            dataType: "json"
          },
          update: {
            url: crudServiceBaseUrl + "/update",
            dataType: "json",
            type: "POST"
          },
          destroy: {
            url: crudServiceBaseUrl + "/delete",
            dataType: "json",
            type: "POST"
          },
          create: {
            url: crudServiceBaseUrl + "/create",
            dataType: "json",
            type: "POST"
          }
      },
      pageSize: 20,
      schema: {
        model: {
          id: "id",
          fields: {
            id: { editable: false, defaultValue: 0 },
            name: { validation: { required: true } },
            position: { field: "position", type: "text", defaultValue: "Operator" },
            status: { editable: false },
            statusColor: { editable: false }
          }
        }
      },
      error: (e) => {
        $(holder).data("kendoGrid").cancelChanges();
        alert(e.xhr.responseText);
        
      }
    })
    resolve(dataSource);
  })
}

export {
  createDataSource
}