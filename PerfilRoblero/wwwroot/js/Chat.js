var datatable;

$(document).ready(function () {
    loadDataTable();
});
function loadDataTable(url) {
    datatable = $('#tblDatos').DataTable({
        "language": {
            "lengthMenu": "Mostrar _MENU_ Mensages Por Pagina",
            "zeroRecords": "Ningun Registro",
            "info": "Mostrar page _PAGE_ de _PAGES_",
            "infoEmpty": "no hay registros",
            "infoFiltered": "(filtered from _MAX_ total registros)",
            "search": "Buscar",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "ajax": {
            "url": "/Admin/Chat/ObtenerTodos"//concatenar con la url que recibe
        },
        "columns": [
            {
                "data": function usuarioAplicacion(data) {
                    return data.usuarioAplicacion.nombres + " " + data.usuarioAplicacion.apellidos;
                }, "width": "20%"
            },
            { "data": "textos", "width": "25%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a class="btn btn-success text-white" style="cursor:pointer">
                               <i class="bi bi-check-all"></i>
                            </a>                           
                        </div>
                        `;
                }, "width": "5%"
            }
        ]
    });
}

