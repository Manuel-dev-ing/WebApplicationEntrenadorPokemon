console.log("Mis Pokemon");
const listaPokemon = document.querySelector("#listaPokemon");


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

obtenerPokemon();

async function obtenerPokemon() {
    crearSpinner()
    const spinnerelement = document.getElementById('div-spinner');

    var url = "/api/pokemon/pok";

    const respuesta = await fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })

    if (respuesta.ok) {


        var data = await respuesta.json();
        data.forEach((type) => {
            let tipos = type.tipo.split(" ")
            let tipoPokemon = tipos.map((pok) => `<p class="${pok} tipo">${pok}</p>`)

            tipoPokemon = tipoPokemon.join()


            const div = document.createElement("div");
            div.classList.add("pokemon");

            div.innerHTML = `

                    <p class="pokemon-id-back">#${type.id}</p>
                    <div class="pokemon-imagen">
                        <img loading="lazy" src="${type.officialArtwork}" alt="${type.nombre}" />
                    </div>

                    <div class="pokemon-info">
                        <div class="nombre-contenedor">
                            <p class="poekmon-id">#${type.id}</p>
                            <h2 class="pokemon-nombre">${type.nombre}</h2>
                        </div>
                        <div class="pokemon-tipos">
                            ${tipoPokemon}
                        </div>
                        <div class="pokemon-stats">
                            <p class="stat">${type.altura}m</p>
                            <p class="stat">${type.peso}kg</p>
                        </div>
                         <div class="">
                               <button class="btn btn-danger btnEliminar" id="${type.nombre}">Eliminar</button>
                         </div>
                    </div>
        `;
            
            listaPokemon.append(div);
            

        })
        spinnerelement.remove();

    }
}

var pok = document.querySelector("#listaPokemon");
cargarEventListener()

function cargarEventListener() {
    pok.addEventListener("click", buscarEliminarPokemon)
}



function buscarEliminarPokemon(e) {
    e.preventDefault();

    if (e.target.classList.contains("btnEliminar")) {


        const pokemonEliminar = e.target.parentElement.parentElement.parentElement

        

        console.log("pokemon seleccionado eliminar: ", pokemonEliminar);

        var id = pokemonEliminar.querySelector('.poekmon-id').textContent

        id = id.replace("#","")

        eliminarPokemon(id, pokemonEliminar)
    }

}

async function eliminarPokemon(id, pokemonEliminar) {
    Swal.fire({
        title: "Realmente deseas Eliminarlo?",
        text: "No podrás revertirlo!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, Eliminarlo!"
    }).then((result) => {
        if (result.isConfirmed) {
            borrar(id, pokemonEliminar)
        }
    });

}

async function borrar(id, pokemon) {

    const url = `/api/pokemon/${id}`;

    console.log("click id:", url)

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
        let mensaje = "Pokemon eliminado Correctamente";
        mostrarMensaje(mensaje)
        pokemon.remove();
    }
}

async function manejarErrorApi(respuesta) {
    var mensajeError = '';

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

function mostrarMensaje(mensaje) {
    Swal.fire({
        icon: "success",
        title: mensaje,
        showConfirmButton: false,
        timer: 1500
    });
}
