﻿//Archivo de funciones para el CRUD de empleado
$(document).ready(function () { //click
    GetAll();
   
});


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
                    + "<td  id='id' class='text-center'>" + empleado.Nombre + "</td>"
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
        url: 'http://localhost:57440/GetById/api/Empleado/' + IdEmpleado,
        success: function (result) {
            $('#txtIdSubCategoria').val(result.Object.IdEmpleado);
            $('#txtNombre').val(result.Object.Nombre);
            $('#txtDescripcion').val(result.Object.ApellidoPaterno);
            $('#txtIdCategoria').val(result.Object.ApellidoMaterno);
            $('#txtIdCategoria').val(result.Object.CatEntidadFederativa.IdEntidad);
            $('#ModalUpdate').modal('show');
        },
        error: function (result) {
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
        }


    });

}
