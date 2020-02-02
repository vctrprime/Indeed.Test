function createDataSource() {
  return new Promise((resolve, reject) => {
    const dataSource = {
      transport: {
        read: "api/requests"
      },
      pageSize: 20
    }
    resolve(dataSource);
  })
}

export {
  createDataSource
}