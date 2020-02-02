function createDataSource() {
  return new Promise((resolve, reject) => {
    const dataSource = {
      transport: {
        read: "api/workers"
      },
      pageSize: 20
    }
    resolve(dataSource);
  })
}

export {
  createDataSource
}