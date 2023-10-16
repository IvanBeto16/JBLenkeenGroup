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
                $("#ddlEntidadFederativa").append('<option value =" ' +
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