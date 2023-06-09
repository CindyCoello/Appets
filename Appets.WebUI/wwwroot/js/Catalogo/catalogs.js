﻿appConfig.Catalogs = (function () {
    var obj = {},
        table,
        formId,
        $editModal,
        $deleteModal,
        action,
        submitBtn;

    function createDataTable(params) {
        $dataTable = $("#datatable");
        table = $dataTable.DataTable();

        $dataTable.on("click", ".edit-btn", function (e) {
            var id = $(this).data("id");
            var btn = Ladda.create($(this)[0]); //button spin animation
            action = "edit";
            $.ajax({
                type: "GET",
                url: params.getUrl + "/" + id,
                dataType: "json",
                beforeSend: function () {
                    btn.start();
                },
                success: function (response) {
                    if (response.success) {
                        assignSettings(response.item);
                        $editModal.modal("show");
                    }
                    else {
                        appConfig.alert(response.type, response.message);
                    }
                },
                complete: function () {
                    btn.stop();
                }
            });
        });

        $("#add-btn").click(function () {
            $(formId).trigger("reset");
            $("#item-id").val("0");
            action = "edit";
        });

        $dataTable.on("click", ".delete-btn", function (e) {
            var id = $(this).data("id");
            var btn = Ladda.create($(this)[0]);
            action = "delete";
            $.ajax({
                type: "GET",
                url: params.getUrl + "/" + id,
                dataType: "json",
                beforeSend: function () {
                    btn.start();
                },
                success: function (response) {
                    if (response.success) {
                        $("#delete-item-id").val(id);
                        $("#delete-item-name").html(response.item.name);
                        $deleteModal.modal("show");
                    }
                    else {
                        appConfig.alert(response.type, response.message);
                    }
                },
                complete: function () {
                    btn.stop();
                }
            });
        });
    }

    // Assing ajax values to inputs in edit form
    var assignValue = function (n, v) {
        var $input = $('#edit-modal form input[name="' + n + '" i]');
        if ($input.length > 0) {
            if ($input.attr("type") == "text" || $input.attr("type") == "hidden") {
                $input.val(v);
            } else if ($input.attr("type") == "checkbox") {
                $input.prop("checked", v);
            }
        }
        else {
            $input = $('#edit-modal form select[name="' + n + '" i]');
            if ($input.length > 0) {
                $input.val(v);
                if ($input.hasClass("selectpicker")) {
                    $input.selectpicker("refresh");
                }
            }
        }
    }

    var assignSettings = function (list, prefix) {
        if (typeof prefix != "undefined") {
            prefix += ".";
        } else {
            prefix = "";
        }
        for (var item in list) {
            if ((typeof list[item] == "string") || (typeof list[item] == "boolean") || typeof list[item] == "number") {
                assignValue(prefix + item, list[item]);
            } else if (typeof list[item] == "object") {
                var n1 = item;
                assignSettings(list[n1], prefix + n1);
            }
        }
    }

    function createEditModal(params) {
        $editModal = $(params.editModalId);
        formId = params.editModalId + " form:first";
        $editModal.on("show.bs.modal", function () {
            var modalTitle = "Agregar ", saveBtnText = "Guardar";
            if ($("#item-id").val() && $("#item-id").val() != "0") {
                modalTitle = "Editar ";
                saveBtnText = "Guardar cambios";
            }

            $(params.editModalId + " .modal-title").html(modalTitle + params.displayName);
            $(params.editModalId + " .modal-footer .btn-primary").html("<i class='mdi mdi-content-save-outline'></i> " + saveBtnText);

            setTimeout(function () {
                $(formId + " *:input[type!=hidden]:first").focus();
            }, 500);
        });

        $editModal.on("hidden.bs.modal", function () {
            $(":input", formId).not(':button, :submit, :reset, input[name="__RequestVerificationToken"]').val("").removeAttr("readonly");

            //$(".selectpicker").val("").selectpicker("refresh");

            var validator = $(formId).validate();
            $('[name]', formId).each(function () {
                validator.successList.push(this);
                validator.showErrors();
            });
            validator.resetForm();
            validator.reset();
            $(".input-validation-error").removeClass("input-validation-error");
        });
    }

    function createDeleteModal(params) {
        $deleteModal = $(params.deleteModalId);
    }

    obj.configure = function (params) {
        //if (params.dataTableId === undefined)
        //    params.dataTableId = "#datatable";

        if (params.editModalId === undefined)
            params.editModalId = "#edit-modal";

        if (params.deleteModalId === undefined)
            params.deleteModalId = "#delete-modal";

        $(function () {
            createDataTable(params);
            createEditModal(params);
            createDeleteModal(params);
        });
    };

    obj.begin = function (xhr, settings) {
        if (action == "edit") {
            submitBtn = Ladda.create($("#edit-modal .ladda-button")[0]);
        }
        else {
            submitBtn = Ladda.create($("#delete-modal .ladda-button")[0]);
        }
        submitBtn.start();
    };

    obj.success = function (data, status, xhr) {
        if (data.success) {
            appConfig.alertSetPositionHeader();
            $editModal.modal("hide");
            $deleteModal.modal("hide");
            //table.ajax.reload();
            reloadTable();
        }
        else {
            appConfig.alertSetPositionTop();
        }

        appConfig.alert(data.type, data.message);
    };

    obj.failure = function (xhr, status, error) {
        console.log(error);
    }

    obj.complete = function (xhr, status) {
        submitBtn.stop();
    };

    return obj;
}());