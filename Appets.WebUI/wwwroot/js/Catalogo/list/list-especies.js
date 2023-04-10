appConfig.Especies = (function () {
    var obj = {};
    //url de donde sacare la data.
    obj.configureTable = function (params) {
        $(function () {
            var exportOptions = { columns: [0, 1, 2], orthogonal: "export" }, canEdit, canDelete;
            var table = $('#datatable').DataTable({
                buttons: [
                    {
                        text: '<i class="mdi mdi- refresh"> Recargar</i>',
                        titleAttr: 'Regargar tabla',
                        action: function (e, dt, node, config) {
                            dt.ajax.reload();
                        }
                    },
                    {
                        extend: "collection",
                        text: "<i class='mdi mdi-export'></i> Exportar",
                        titleAttr: "Exportar esta tabla",
                        buttons: [
                            {
                                extend: "excelHtml5",
                                text: "<i class='mdi mdi-file-excel-outline'></i> Excel",
                                exportOptions: exportOptions
                            },
                            {
                                extend: "csvHtml5",
                                text: "<i class='mdi mdi-file-multiple-outline'></i> CSV",
                                exportOptions: exportOptions
                            },
                            {
                                extend: "pdfHtml5",
                                text: "<i class='mdi mdi-file-pdf-outline'></i> PDF",
                                exportOptions: exportOptions
                            }
                        ]
                    },
                    {
                        text: '<i class="mdi mdi-plus-thick" id="add-btn" data-toggle="modal" data-target="#edit-modal"> Nuevo</i>',
                        attr: {
                            title: "Añadir nueva especie",
                            id: "add-btn"
                        }
                    }
                ],
                ajax: function (data, callback, settings) {
                    $.ajax({
                        url: params.listUrl,
                        type: "GET",
                        dataType: "json",
                        success: function (response) {
                            callback(response);
                            table.column(-1).visible(true);
                        },
                    });
                },
                columnDefs: [
                    {
                        targets: 0,
                        data: 'espc_Id'
                    },
                    {
                        targets: 1,
                        data: 'espc_Descripcion'
                    },
                    {
                        targets: 2,
                        className: "text-center",
                        width: 80,
                        render: function (data, type, row) {
                            botones = "";
                            if (type == "display") {
                                botones += '<button class="btn btn-secondary btn-sm edit-btn ladda-button" data-style="zoom-in" data-id="' + row.espc_Id + '"><span class"ladda-label"><i class="mdi mdi-square-edit-outline"></i></span></button> | ';
                                botones += '<button title="Eliminar" class="btn btn-outline-danger btn-sm" onclick="Modal(' + row.espc_Id + ')"><i class="mdi mdi-delete-forever"></i></button>';
                               // botones += '<button class="btn btn-danger btn-sm" onclick="EliminarRole(' + row.comp_Id + ')"><i class="mdi mdi-delete-empty"></i></button>';
                            }
                            return botones;
                        }
                    }
                ]
            });
        });
        $('#datatable').on('init.dt', function () {
            $('#add-btn')
                .attr('data-toggle', 'modal')
                .attr('data-target', '#edit-modal');
        });
    };

    return obj;
}());

//function Redireccionar() {
//    window.location = "/Especies/AgregarEspecie";
//}