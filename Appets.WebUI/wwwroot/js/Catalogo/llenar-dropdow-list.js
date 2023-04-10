
$('#DropEspecie').change(function () {
    var especie = $(this).val();
    console.log(especie);
    $('#DropRaza').empty();
    $('#DropRaza').append('<option>Seleccione una raza</option>');
    $.getJSON("/Mascotas/RazasList/" + especie, function (data) {
        $.each(data, function (key, value) {
            $('#DropRaza').append('<option value="' + value.raza_Id + '">' + value.raza_Descripcion + '</option>');
        });
    });
})



function Modal(id) {
    $("#ModalEliminar").modal();
    $("#ID").val(id);
}


function Eliminar() {
    var data = {
        id: $("#ID").val()
    }
    console.log(data);
    $.ajax({
        url: "/Especies/Eliminar",
        type: "POST",
        dataType: "json",
        data: data,
    }).done(function (result) {
        if (result != null) {
            console.log(result);
            if (result == "1") {
                $("#ModalEliminar").modal("hide");
                $('#dataTable').DataTable().ajax.reload();
                appConfig.alert('success', 'El registro se elimino exitosamente');
                reloadTable();

            }
            else {
                $("#ModalEliminar").modal("hide");
                appConfig.alert('error', 'No se pudo eliminar el registro');
            }
        }
    })
}






//function Eliminar() {
//    var data = {
//        id: $("#ID").val()
//    }
//    console.log(data);
//    $.ajax({
//        url: "/Voluntario/Eliminar",
//        type: "POST",
//        dataType: "json",
//        data: data,
//    }).done(function (result) {
//        if (result != null) {
//            console.log(result);
//            if (result == "1") {
//                $("#ModalEliminar").modal("hide");
//                $('#dataTable').DataTable().ajax.reload();
//                appConfig.alert('success', 'El registro se elimino exitosamente');
//            }
//            else {
//                $("#ModalEliminar").modal("hide");
//                appConfig.alert('error', 'No se pudo eliminar el registro');
//            }
//        }
//    })
//}



//function Eliminar() {
//    var data = {
//        id: $("#ID").val()
//    }
//    console.log(data);
//    $.ajax({
//        url: "/Mascotas/Eliminar",
//        type: "POST",
//        dataType: "json",
//        data: data,
//    }).done(function (result) {
//        if (result != null) {
//            console.log(result);
//            if (result == "1") {
//                $("#ModalEliminar").modal("hide");
//                $('#dataTable').DataTable().ajax.reload();
//                appConfig.alert('success', 'El registro se elimino exitosamente');
//            }
//            else {
//                $("#ModalEliminar").modal("hide");
//                appConfig.alert('error', 'No se pudo eliminar el registro');
//            }
//        }
//    })
//}



//function Eliminar() {
//    var data = {
//        id: $("#ID").val()
//    }
//    console.log(data);
//    $.ajax({
//        url: "/FichaAdopcion/Eliminar",
//        type: "POST",
//        dataType: "json",
//        data: data,
//    }).done(function (result) {
//        if (result != null) {
//            console.log(result);
//            if (result == "1") {
//                $("#ModalEliminar").modal("hide");
//                $('#dataTable').DataTable().ajax.reload();
//                appConfig.alert('success', 'El registro se elimino exitosamente');
//            }
//            else {
//                $("#ModalEliminar").modal("hide");
//                appConfig.alert('error', 'No se pudo eliminar el registro');
//            }
//        }
//    })
//}



//function Eliminar() {
//    var data = {
//        id: $("#ID").val()
//    }
//    console.log(data);
//    $.ajax({
//        url: "/FichaMedica/Eliminar",
//        type: "POST",
//        dataType: "json",
//        data: data,
//    }).done(function (result) {
//        if (result != null) {
//            console.log(result);
//            if (result == "1") {
//                $("#ModalEliminar").modal("hide");
//                $('#dataTable').DataTable().ajax.reload();
//                appConfig.alert('success', 'El registro se elimino exitosamente');
//            }
//            else {
//                $("#ModalEliminar").modal("hide");
//                appConfig.alert('error', 'No se pudo eliminar el registro');
//            }
//        }
//    })
//}



//function Eliminar() {
//    var data = {
//        id: $("#ID").val()
//    }
//    console.log(data);
//    $.ajax({
//        url: "/Persona/Eliminar",
//        type: "POST",
//        dataType: "json",
//        data: data,
//    }).done(function (result) {
//        if (result != null) {
//            console.log(result);
//            if (result == "1") {
//                $("#ModalEliminar").modal("hide");
//                $('#dataTable').DataTable().ajax.reload();
//                appConfig.alert('success', 'El registro se elimino exitosamente');
//            }
//            else {
//                $("#ModalEliminar").modal("hide");
//                appConfig.alert('error', 'No se pudo eliminar el registro');
//            }
//        }
//    })
//}