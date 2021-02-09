
class UI {
    static alertToaster(type, message, title) {
        toastr[`${type}`](`${message}`, `${title}`),
            toastr.options =
            {
                closeButton: false,
                debug: false,
                newestOnTop: false,
                progressBar: false,
                positionClass: "toast-top-right",
                preventDuplicates: false,
                onclick: null,
                showDuration: 300,
                hideDuration: 1000,
                timeOut: 5000,
                extendedTimeOut: 1000,
                showEasing: "swing",
                hideEasing: "linear",
                showMethod: "fadeIn",
                hideMethod: "fadeOut"
            };
    }

    static activateTable(tablename) {
        var table;
        if ($.fn.dataTable.isDataTable(`#${tablename}`)) {
            table = $(`#${tablename}`).DataTable({
            });
        } else {
            table = $(`#${tablename}`).DataTable({
                paging: true,
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Turkish.json"
                }
            });
        }
        return table;
    }


}