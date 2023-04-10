appConfig.FichaMedica = (function () {
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
                        text: '<i class="mdi mdi-plus-thick">Nuevo</i>',
                        attr: {
                            title: "Añadir nueva Persona",
                            onclick: "redireccionar()"
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
                        data: 'medic_Id'
                    },
                    {
                        targets: 1,
                        data: 'masc_Id'
                    },
                    {
                        targets: 2,
                        data: 'medic_Esterilizacion'
                    },
                    {
                        targets: 3,
                        data: 'medic_Personalidad'
                    },
                    {
                        targets: 4,
                        data: 'medic_SaludCuidado'
                    },
                    
                    
                    {
                        targets: 5,
                        className: "text-center",
                        width: 80,
                        render: function (data, type, row) {
                            botones = "";
                            if (type == "display") {
                                botones += '<a class="btn btn-secondary btn-sm" href="/FichaMedica/EditarFichaMedica/' + row.medic_Id + '"><i class="mdi mdi-square-edit-outline"></i></a> | ';
                                botones += '<button title="Eliminar" class="btn btn-outline-danger btn-sm" onclick="Modal(' + row.medic_Id + ')"><i class="mdi mdi-delete-forever"></i></button>';

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


function redireccionar() {
    window.location = '/FichaMedica/AgregarFichaMedica';
}