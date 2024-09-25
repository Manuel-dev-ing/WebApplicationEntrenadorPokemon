console.log("MIS ITEMS")
const listaItems = document.querySelector("#listaItems");


function crearSpinner() {
    const divcontenedor = document.getElementById('id-spinner');

    const divSpinner = document.createElement('div');
    divSpinner.className = 'd-flex justify-content-center mt-5 mb-5';
    divSpinner.id = 'div-spinner';

    const spinner = document.createElement('div');
    spinner.className = 'spinner-border';
    spinner.setAttribute('role', 'status');
    spinner.id = 'spinner';

    const span = document.createElement('span');
    span.className = 'visually-hidden';

    spinner.appendChild(span);

    divSpinner.appendChild(spinner);

    divcontenedor.appendChild(divSpinner);

}



async function buscarItems() {
   
    crearSpinner()
    const spinnerelement = document.getElementById('div-spinner');

    var url = "/api/item/misItems"

    const respuesta = await fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })


    if (respuesta.ok) {
        var data = await respuesta.json();

        MisItems(data);
        spinnerelement.remove();

    }
    //var data = await respuesta.json();
    //console.log("RESPUESTA: ")
}

buscarItems();


function MisItems(data) {
    data.forEach((res) => mostrarMisItems(res))

}

function mostrarMisItems(res) {

    var itemId = res.itemId;
    var descripcion = res.descripcion;
    var nombre = res.nombre;
    var sprite = res.sprite;
    var totalItems = res.totalItemId;

    const div = document.createElement("div");
    div.classList.add("item", "position-relative");
    div.innerHTML = `
                    <p class="item-id-back">#${itemId}</p>
                    <div class="item-imagen">
                        <img loading="lazy" src="${sprite}" alt="${nombre}" />
                    </div>

                    <div class="item-info">
                        <h2 class="item-nombre">${nombre}</h2>

                        <span class="badge bg-light text-dark">quantity: ${totalItems}</span>

                        <div class="nombre-contenedor">
                            <p class="text-description">Description:</p>
                            <p class="item-descripcion">${descripcion}</p>
                        </div>
                       
                        
                         <div>
                               <button class="btn btn-danger btnEliminar" id="${nombre}" >Eliminar</button>
                         </div>
                    </div>
    `;

    listaItems.append(div);


}

var item = document.querySelector("#listaItems");


cargarEventListeners()
function cargarEventListeners() {
    item.addEventListener("click", buscarEliminarItem)


}

function buscarEliminarItem(e) {
    e.preventDefault();
    if (e.target.classList.contains("btnEliminar")) {
        var itemSeleccionado = e.target.parentElement.parentElement.parentElement

        var id = itemSeleccionado.querySelector('.item-id-back').textContent

        id = id.replace("#", "")

        eliminarItem(id)
    }
}


async function eliminarItem(id) {
    Swal.fire({
        title: "Realmente deseas eliminar el item?",
        text: "No podrás revertirlo!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, Eliminarlo!"
    }).then((result) => {
        if (result.isConfirmed) {
           borrar(id)
        }
    });

}

async function borrar(id) {
    const url = `/api/item/${id}`;
   

    const respuesta = await fetch(url, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        }
    })
    
    if (!respuesta.ok) {
        manejarErrorApi(respuesta)
        return;
    } else {
        mensaje()
        const data = await respuesta.json();
        console.log(data);
        listaItems.innerHTML = "";
        MisItems(data);
    }
}



function mensaje() {
    
    Swal.fire({
        title: "Eliminado!",
        text: "Ha sido eliminado correctamente.",
        icon: "success"
    });
        
    
}
async function manejarErrorApi(respuesta) {
    let mensajeError = '';

    if (respuesta.status === 400) {
        mensajeError = await respuesta.text();

    } else if (respuesta.status === 404) {
        mensajeError = recursoNoEncontrado;
    } else {
        mensajeError = errorInesperado;
    }

    mostrarMensajeError(mensajeError);

}

function mostrarMensajeError(mensajeError) {
    Swal.fire({
        icon: "error",
        title: "Ocurrio un error inesperado",
        text: mensajeError,
    });
}











