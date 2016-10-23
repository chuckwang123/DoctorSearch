var baseUrl = 'http://localhost:57251/'; // default to relative path
var API_URL = baseUrl + '/api/doctors';
var jsonData = '';


$(function () {
    GenerateIndexPage();
    
    $('li').click(function () {
        var grepFunc;

        switch ($(this).index()) {
            case 0:
                grepFunc = function (item) {
                    return ageFormatter(item.Birth) >= 20 && ageFormatter(item.Birth) <= 30;
                };
                break;
            case 1:
                grepFunc = function (item, i) {
                    return ageFormatter(item.Birth) > 30 && ageFormatter(item.Birth) <= 40;
                };
                break;
            case 2:
                grepFunc = function (item) {
                    return ageFormatter(item.Birth) > 40 && ageFormatter(item.Birth) <= 50;
                };
                break;
            case 3:
                grepFunc = function (item) {
                    return ageFormatter(item.Birth) > 50 && ageFormatter(item.Birth) <= 60;
                };
                break;
            case 4:
                grepFunc = function (item) {
                    return ageFormatter(item.Birth) > 60;
                };
                break;
        }
        $('#table').bootstrapTable('load', $.grep(jsonData, grepFunc));
    });
});

function GenerateIndexPage() {
    GeneratedocTemplate();
    LoadTable();
}

function GeneratedocTemplate() {
    var doctorTemplate = '<div class="table-content"><a class="nav-link"><h3 class="page-header">Doctor Dashboard</h3><tbody> </a> <div class="dropdown">  <button class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown">Age Select<span class = "caret"></span></button>  <ul class="dropdown-menu" role = "menu">    <li><a href="#">20-30</a></li>    <li><a href="#">30-40</a></li>    <li><a href="#">40-50</a></li>    <li><a href="#">50-60</a></li>    <li><a href="#">60+</a></li>  </ul></div><table id="table" data-pagination="true" data-show-refresh="true" data-sort-name="Name" data-sort-order="asc" data-search="true" data-toolbar=".toolbar" data-filter-control="true"> <thead> <tr id="header"> <th data-field="Name" data-width="11%" data-sortable="true">Name <i aria-hidden="true"></i></i> </th> <th data-field="Birth" data-align="center" data-formatter="ageFormatter" data-width="16%" data-sortable="true" >Age <i aria-hidden="true"></i> </th> <th data-field="Sex" data-align="center" data-filter-control="select" data-width="14%"  data-sortable="true" >Sex <i aria-hidden="true"></i> </th> <th data-field="TEL" data-align="center" data-width="14%" data-sortable="true" >TEL <i aria-hidden="true"></i> </th> <th data-field="AreaCode" data-align="center" data-width="14%" data-sortable="true" >Area Code <i aria-hidden="true"></i> </th> <th data-field="Score" data-align="center" data-filter-control="select" data-width="14%" data-sortable="true" >Score <i aria-hidden="true"></i> </th> <th data-field="Specialties" data-align="center" data-width="14%" data-formatter="SpecialtiesFormatter" data-sortable="true" >Specialties <i aria-hidden="true"></i> </th> </tr> </thead> </table></div>';
    Mustache.parse(doctorTemplate);   // optional, speeds up future uses
    var rendered = Mustache.render(doctorTemplate);
    $('#content').html(rendered);
    $('body').css('background-color', '#fff');
}

function LoadTable() {
    GetData(API_URL).done(function (data) {
        jsonData = data;
        var $table = $('#table').bootstrapTable({
            url: API_URL,
        });
       }).fail(function (data) {
           alert("error");
    });
}

function GetData(apiUrl) {
    return $.ajax({
        url: apiUrl,
        type: "GET",
        crossDomain: true,
        contentType: "application/json"
    });
}

function ageFormatter(value) {
    var today = new Date();
    var birthDate = new Date(value);
    var age = today.getFullYear() - birthDate.getFullYear();
    var m = today.getMonth() - birthDate.getMonth();
    if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
        age--;
    }
    return age;
}

function SpecialtiesFormatter(value) {
    var result = "";
    value.forEach(function(specialty) {
        result += specialty.Name + ", ";
    });
    return result;
}

function ageSelection() {
    var grepFunc;

    switch ($(this).index()) {
        case 0:
            grepFunc = function (item) {
                return ageFormatter(item.Birth) >= 20 && ageFormatter(item.Birth) <= 30;
            };
            break;
        case 1:
            grepFunc = function (item, i) {
                return ageFormatter(item.Birth) > 30 && ageFormatter(item.Birth) <= 40;
            };
            break;
        case 2:
            grepFunc = function (item) {
                return ageFormatter(item.Birth) > 40 && ageFormatter(item.Birth) <= 50;
            };
            break;
        case 3:
            grepFunc = function (item) {
                return ageFormatter(item.Birth) > 50 && ageFormatter(item.Birth) <= 60;
            };
            break;
        case 4:
            grepFunc = function (item) {
                return ageFormatter(item.Birth) > 60;
            };
            break;
    }
    $('#table').bootstrapTable('load', $.grep(jsonData, grepFunc));
}