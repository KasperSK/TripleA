var StatistikViewModel = function() {
    var self = this;


    self.SalesOrders = ko.observableArray();
    self.OrderLines = ko.observableArray();
    self.Transactions = ko.observableArray();

    self.SalesOrdersDetails = ko.observable();

    self.SalesOrdersWithStatus = ko.observableArray();
    self.TransactionsWithStrings = ko.observableArray();

    self.OrderLinesFromSalesOrder = ko.observableArray();
    self.TransactionFromSalesOrder = ko.observableArray();


    self.error = ko.observable();

    var salesOrderUri = '/api/salesorders/';
    var orderLineUri = '/api/orderlines';
    var transactionUri = '/api/transactions';

    function ajaxHelper(uri, method, data) {
        self.error('');
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    };

    function listCompare(koList, compareList, thisList) {
        thisList([]);

        ko.utils.arrayForEach(koList, function (item) {
            compareList.forEach(function (entry) {
                if (item.Id == entry) {
                    thisList.push(item);
                };
            });
        });
    };

    function getStatusString(status) {
        switch (status) {
            case 0:
                return "Created";
                break;
            case 1:
                return "Completed";
                break;
            case 2:
                return "Failed";
                break;
            default:
                return "";
        }
    }

    function getPaymentType(type) {
        switch (type) {
            case 0:
                return "Cash";
                break;
            case 1:
                return "Nets";
                break;
            case 2:
                return "Swipp";
                break;
            case 3:
                return "MobilePay";
                break;
            default:
                return "";
        }
    }



    function getAllSalesOrders() {
        ajaxHelper(salesOrderUri, 'GET').done(function (data) {
            self.SalesOrders(data);
            ko.utils.arrayForEach(self.SalesOrders(), function(item) {
                var salesDisplay =
                {
                    Id: item.Id,
                    Date: item.Date.substring(0, 10),
                    Time: item.Date.substring(11,20),
                    Status: getStatusString(item.Status),
                    Total: item.Total
                }
                self.SalesOrdersWithStatus.push(salesDisplay);
            });
        });
    };

    function getAlleTransactions() {
        ajaxHelper(transactionUri, 'GET').done(function(data) {
            self.Transactions(data);
            ko.utils.arrayForEach(self.Transactions(), function (item) {
                var transactionDisplay =
                {
                    Id: item.Id,
                    Date: item.Date.substring(0, 10),
                    Time: item.Date.substring(11, 20),
                    Price: item.Price,
                    Status: getStatusString(item.Status),
                    PaymentType: getPaymentType(item.PaymentType)
                }
                self.TransactionsWithStrings.push(transactionDisplay);
            });
        });
    }

    function getAllOrderLines() {
        ajaxHelper(orderLineUri, 'GET').done(function(data) {
            self.OrderLines(data);
        });
    }

    self.getSalesOrderDetails = function (item) {
        ajaxHelper(salesOrderUri + item.Id , 'GET').done(function (data) {
            self.SalesOrdersDetails(data);
            listCompare(self.OrderLines(), self.SalesOrdersDetails().Lines, self.OrderLinesFromSalesOrder);
            listCompare(self.TransactionsWithStrings(), self.SalesOrdersDetails().Transactions, self.TransactionFromSalesOrder);
        });
    };

    getAlleTransactions();
    getAllSalesOrders();
    getAllOrderLines();

    
}

ko.applyBindings(new StatistikViewModel());