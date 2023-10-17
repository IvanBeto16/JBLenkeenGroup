//Archivo de funciones para el CRUD de empleado
$(document).ready(function () { //click
    GetAll();
    EntidadFederativaGetAll();
});


function EntidadFederativaGetAll() {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:57440/api/EntidadFederativa',
        success: function (result) {
            $('#ddlEntidadFederativa').append('<option value = "' + 0 + '">' + 'Seleccione una opcion' + '</option>');
            $.each(result.Objects, function (i, entidadFederativa) {
                $("#ddlEntidadFederativa").append('<option value ="' +
                    + entidadFederativa.IdEntidad + '">'
                    + entidadFederativa.Estado + '</opcion>');
            });
        }
    });
}

function GetAll() {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:57440/api/Empleado/GetAll',
        success: function (result) { //200 OK 
            $.each(result.Objects, function (i, empleado) {
                var filas =
                    '<tr>'
                    + '<td class="text-center"> '
                    + '<a href="#" onclick="GetById(' + empleado.IdEmpleado + ')">'
                    + '<i class="bi bi-vector-pen">Actualizar usuario</i>'
                    + '</a> '
                    + '</td>'
                    + "<td class='text-center'>" + empleado.NumeroNomina + "</td>"
                    + "<td class='text-center'>" + empleado.Nombre + "</td>"
                    + "<td class='text-center'>" + empleado.ApellidoPaterno + "</td>"
                    + "<td class='text-center'>" + empleado.ApellidoMaterno + "</ td>"
                    + "<td class='text-center'>" + empleado.CatEntidadFederativa.IdEntidad + "</td>"
                    + '<td class="text-center"> <button class="btn btn-danger" onclick="Eliminar(' + empleado.IdEmpleado + ')"><i class="fa-solid fa-trash-can"></i></button></td>'

                    + "</tr>";
                $("#tblEmpleados tbody").append(filas);
            });
        },
        error: function (result) {
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
        }
    });
};

function GetById(IdEmpleado) {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:57440/api/Empleado/' + IdEmpleado,
        success: function (result) {
            $('#numeroEmpleado').val(result.Object.IdEmpleado);
            $('#numeroNomina').val(result.Object.NumeroNomina);
            $('#nombreEmpleado').val(result.Object.Nombre);
            $('#apellidoPaterno').val(result.Object.ApellidoPaterno);
            $('#apellidoMaterno').val(result.Object.ApellidoMaterno);
            $('#ddlEntidadFederativa').val(result.Object.CatEntidadFederativa.IdEntidad);
            $('#formulario').modal('show');
        },
        error: function (result) {
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
        }


    });

}
function AddEmpleado() {
    var nuevoEmpleado = {
        Id: 0,
        NumeroNomina: $('#numeroNomina').val(),
        Nombre: $('#nombreEmpleado').val(),
        ApellidoPaterno: $('#apellidoPaterno').val(),
        ApellidoMaterno: $('#apellidoMaterno').val(),
        CatEntidadFederativa: {
            IdEntidad: $('#ddlEntidadFederativa').val()
        }
    };

    $.ajax({
        type: 'POST',
        url: 'http://localhost:57440/api/Empleado',
        dataType: 'json',
        data: nuevoEmpleado,
        success: function (result) {
            window.alert("Empleado agregado");
            $('#formulario').modal('hide');
            GetAll();

        },
        error: function (result) {
            alert('Error al agregar empleado: ' + result.ErrorMessage);
        }
    });
}

function UpdateEmpleado() {
    var empleado = {
        IdEmpleado: $('#numeroEmpleado').val(),
        Nombre: $('#nombreEmpleado').val(),
        ApellidoPaterno: $('#apellidoPaterno').val(),
        ApellidoMaterno: $('#apellidoMaterno').val(),
        NumeroNomina: $('#numeroNomina').val(),
        CatEntidadFederativa: {
            IdEntidad: $('#ddlEntidadFederativa').val()
        }
    }

    $.ajax({
        type: 'PUT',
        url: 'http://localhost:57440/api/Empleado/' + empleado.IdEmpleado,
        datatype: 'JSON',
        data: empleado,
        success: function (result) {
            alert('Empleado actualizado correctamente');
            $('#formulario').modal('hide');
            GetAll();
        },
        error: function (result) {
            alert('Error al actualizar ' + result.responseJSON.ErrorMessage);
        }
    });
}


function Eliminar(IdEmpleado) {

    if (confirm("¿Estas seguro de eliminar el empleado seleccionado?")) {
        $.ajax({
            type: 'DELETE',
            url: 'http://localhost:57440/api/Empleado/' + IdEmpleado,
            success: function (result) {
                window.alert("Empleado eliminado correctamente");
                $('#formulario').modal();
                GetAll();
            },
            error: function (result) {
                alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
            }
        });

    };
};

function Cambios() {
    var bandera = $('#numeroEmpleado').val();
    if (bandera == 0) {
        AddEmpleado();
    } else {
        UpdateEmpleado();
    }
    document.getElementById('nombreEmpleado').value = '';
    document.getElementById('numeroEmpleado').value = '';
    document.getElementById('apellidoPaterno').value = '';
    document.getElementById('apellidoMaterno').value = '';
    document.getElementById('numeroNomina').value = '';
    $('#formulario').modal('hide');
};