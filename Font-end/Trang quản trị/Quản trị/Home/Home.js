var subApp = angular.module('Home', []);

subApp.controller('HomeController', function ($scope) {
  // Controller logic for chart view
  $scope.chartMonths = [
    { name: 'Tháng 1', value: 50 },
    { name: 'Tháng 2', value: 70 },
    { name: 'Tháng 3', value: 90 },
    { name: 'Tháng 4', value: 100 },
    { name: 'Tháng 5', value: 60 },
    { name: 'Tháng 6', value: 120 },
    { name: 'Tháng 7', value: 80 },
    { name: 'Tháng 8', value: 87 },
    { name: 'Tháng 9', value: 54 },
    { name: 'Tháng 10', value: 50 },
    { name: 'Tháng 11', value: 70 },
    { name: 'Tháng 12', value: 90 },
  ];
});
