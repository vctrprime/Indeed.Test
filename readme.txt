1)В качестве распределителя запросов реализация интерфейса IHostedService
2)Хранилище данных - файл data.json, статический класс Context, чтобы не напрягать постоянным считыванием.
Не видел смысла подключать ORM к условному SQLite, вполне достаточно такого подхода, для такого объема данных.
3)DAL через UnitOfWork c generic репозиторием.
4)Тестами покрыл один контроллер, остальные аналогичны
5)UI: sass + jquery + kendo + signalr, упаковано webpack
6)В основном проекте вроде предусмотрел все "mistake-proofing", в консоль не стал уделять этому много времени.

API endpoints:
1) POST создание запроса - api/requests/create 
пример body: {   
    "name": "test1",
    "createdBy": "autotest"
}
2) GET отмена запроса - api/requests/cancel/{id}
3) GET получение запроса - api/requests/get/{id}
