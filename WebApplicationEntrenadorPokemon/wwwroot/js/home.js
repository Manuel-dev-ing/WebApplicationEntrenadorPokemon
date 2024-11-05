console.log("HOME");

const listaPokemon = document.querySelector("#listaPokemon");

var offset = 0;
var limit = 20;


const arrayPaginaAnterior = []
const arrayPaginaSiguiente = []
var Datospok;

var url = "https://pokeapi.co/api/v2/pokemon/?offset=" + offset + "&limit=" + limit;
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
async function buscarPokemon() {
    crearSpinner()
    const spinnerelement = document.getElementById('div-spinner');
    
    const respuesta = await fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })

    if (respuesta.ok) {

        var data = await respuesta.json();
        
        Datospok = data.result;
        offset = 20
        

        pokemons(data)
        spinnerelement.remove();

        arrayPaginaAnterior.push(data.previous);
        paginaAnteriorPokemon()

        paginaSiguientePokemon()
        arrayPaginaSiguiente.push(data.next);

    }

}

buscarPokemon()

function desabilitarBtnPrevious(bool) {
    var btnprevious = document.getElementById('previous')
    btnprevious.disabled = bool

}

function desabilitarBtnNext(bool) {
    var btnprevious = document.getElementById('next')
    btnprevious.disabled = bool

}

function pokemons(data) {

    data = data.results
    console.log("data pokemons: ", data)

    data.forEach((res) => mostrarPokemon(res))

}

function paginaAnteriorPokemon() {
    var btnprevious = document.getElementById('previous');

    var previous = arrayPaginaAnterior[arrayPaginaAnterior.length - 1]

    previous = previous != null ? desabilitarBtnPrevious(false) : desabilitarBtnPrevious(true);
    btnprevious.addEventListener('click', async function () {

        crearSpinner()

        const spinnerelement = document.getElementById('div-spinner');

        var urlAnterior = arrayPaginaAnterior[arrayPaginaAnterior.length - 1]

        const divPoke = document.querySelectorAll('.pokemon');

        divPoke.forEach((divs) => divs.remove())

        const respuesta = await fetch(urlAnterior, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        })
        if (respuesta.ok) {
            arrayPaginaAnterior.pop()

            var data = await respuesta.json();
            
            console.log("offset pagina siguiente: ", offset -= 20)
            previous = data.previous != null ? desabilitarBtnPrevious(false) : desabilitarBtnPrevious(true);
            desabilitarBtnNext(false)

            arrayPaginaSiguiente.push(data.next);
            pokemons(data);
            //arrayDatos.push(data);
            spinnerelement.remove();

        }

    })

}

function paginaSiguientePokemon() {
    var btnNext = document.getElementById('next');

    btnNext.addEventListener('click', async function () {
        crearSpinner()

        const spinnerelement = document.getElementById('div-spinner');

        var urlSiguiente = arrayPaginaSiguiente[arrayPaginaSiguiente.length - 1]

        const divPoke = document.querySelectorAll('.pokemon');
        divPoke.forEach((divs) => divs.remove())

        const respuesta = await fetch(urlSiguiente, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        })

        if (respuesta.ok) {
            spinnerelement.remove();
            var data = await respuesta.json();
            

            arrayPaginaSiguiente.pop()

            arrayPaginaSiguiente.push(data.next);

            arrayPaginaAnterior.push(data.previous);

            //
            const ultimosPokemons = [];
            var datos = data.results;
            offset += 20;

            if (offset == 160) {
                
                desabilitarBtnNext(true)

                for (var i = 0; i <= 10; i++) {
                    ultimosPokemons.push(datos[i])
                }

                ultimosPokemons.forEach((res) => mostrarPokemon(res))

            } else {

                pokemons(data);

            }
            //

            //arrayDatos.push(data);

            var previous = arrayPaginaAnterior[arrayPaginaAnterior.length - 1]
            if (previous != null) {
                desabilitarBtnPrevious(false)
            }
        }

    })



}

async function mostrarPokemon(res) {
    var url = res.url;
    
    const respuesta = await fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })

    if (respuesta.ok) {

        var data = await respuesta.json();
        mostrar(data)
        
    }
}

function mostrar(data) {
    let tipos = data.types.map((type) => `<p class="${type.type.name} tipo">${type.type.name}</p>`)

    tipos = tipos.join('');

    let pokemonid = data.id.toString();
    if (pokemonid.length === 1) {
        pokemonid = "00" + pokemonid;
    } else if (pokemonid.legth === 2) {
        pokemonid = "0" + pokemonid;
    }

    const div = document.createElement("div");
    div.classList.add("pokemon");
    div.innerHTML = `
                    <p class="pokemon-id-back">#${data.id}</p>
                    <div class="pokemon-imagen">
                        <img loading="lazy" src="${data.sprites.other["official-artwork"].front_default}" alt="${data.name}" />
                    </div>

                    <div class="pokemon-info">
                        <div class="nombre-contenedor">
                            <p class="poekmon-id">#${data.id}</p>
                            <h2 class="pokemon-nombre">${data.name}</h2>
                        </div>
                        <div class="pokemon-tipos">
                            ${tipos}
                        </div>
                        <div class="pokemon-stats">
                            <p class="stat">${data.height}m</p>
                            <p class="stat">${data.weight}kg</p>
                        </div>
                         <div class="">
                               <button class="btn btn-primary btnGuardar" id="${data.name}" >Guardar</button>
                         </div>
                    </div>
    `;

    listaPokemon.append(div);
    guardarPokemon(data);
}

async function obtenertiposPokemon() {

    var url = "/api/pokemon/";

    const respuesta = await fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })

    var tiposPokemon = await respuesta.json();
    tiposPokemon.forEach(tipo => {
        console.log(tipo.tipoPokemon)
    })
    filtrarTipoPokemon(tiposPokemon);
    
}

obtenertiposPokemon();

function filtrarTipoPokemon(tiposPokemon) {
    document.getElementById('tipo-pokemon').addEventListener("keyup", (event) => {
        var query = event.target.value.toLowerCase();

        if(query != "") {
            const filterpokemon = tiposPokemon.filter((tipo) => tipo.tipoPokemon.includes(query));
            console.log(filterpokemon)
            mostrarTiposPokemon(filterpokemon)
        } else {
            query = "";
            mostrarTiposPokemon(query);


        }
        
       
        //console.log("query: ", tipospokemon);
    })
}

function mostrarTiposPokemon(filterpokemon) {

    const lista = document.getElementById('lista');
    lista.innerHTML = '';
    if (filterpokemon != "") {
        filterpokemon.forEach(tipo => {
            console.log("TIPO: ", tipo.tipoPokemon)
            const li = document.createElement('li');
            li.classList.add("list-group-item", "tiposPokemon")
            li.setAttribute("id", "listaTipoPokemon")
            li.textContent = tipo.tipoPokemon;
            lista.appendChild(li);
        })

        const tiposPoekmon = document.querySelectorAll(".tiposPokemon")
        tiposPoekmon.forEach(boton => boton.addEventListener("click", (event) => {
            var tipo = event.target.innerText

            console.log("Mostrar tiposPokemon: ", tipo);
            li.remove();
            //let url = Datospok.map((pok) => pok.url);
            myfunction(tipo)

        }))
    }
   
}

async function myfunction(tipo) {
    var tipoPokemon = tipo
    console.log("CLICK TIPO: ", tipoPokemon);
    crearSpinner()
    const spinnerelement = document.getElementById('div-spinner');
    listaPokemon.innerHTML = "";
    var url = "https://pokeapi.co/api/v2/pokemon/"

    for (var i = 1; i < 151; i++) {
        const respuesta = await fetch(url + i, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });

        var data = await respuesta.json();
        const tipos = data.types.map(type => type.type.name);
        if (tipos.some(tipo => tipo.includes(tipoPokemon))) {
            mostrar(data);
            console.log("myfunction data: ", data)

        }
    }
        
   spinnerelement.remove();

}


async function guardarPokemon(data) {
    
    var name = data.name;
    var url = "/api/pokemon";


    var boton = document.getElementById(`${name}`)
    boton.addEventListener("click", async function () {
        let tipos = data.types.map((type) => type.type.name)
        tipos = tipos.join(' ');

        const requestData = {
            pokemonId: data.id,
            nombre: data.name,
            tipos: tipos,
            altura: data.height,
            peso: data.weight,
            imagen: data.sprites.other["official-artwork"].front_default
        }

        console.log("request data: ")
        console.log(requestData)
        var datos = JSON.stringify(requestData)
        

        const respuesta = await fetch(url, {
            method: 'POST',
            body: datos,
            headers: {
                'Content-Type': 'application/json'
            }

        })

        if (respuesta.ok) {
            var mensaje = "Guardado exitosamente"

            mostrarMensaje(mensaje);
        }

        if (!respuesta.ok) {
            var result = await respuesta.json();

            if (result.code == 400) {

                console.log("Si entro en el if result.code")
                template = `
                    <div class="alert alert-warning alert-dismissible fade show col-8" id="alerta">
                        <strong>${result.mensaje}</strong>
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    </div>
                `;
            }
            $('#alertcontenedor').append(template);

            var alerta = document.getElementById('alerta');
            console.log("alerta: ", alerta)

            setTimeout(() => {

                $('#alerta').hide();
                alerta.remove();

            }, 10000);
        }

    

        

        
        
        

        /*if (respuesta.ok) {
            const json = await respuesta.json();
            console.log("respuesta OK: ")
            console.log(json)
        }*/
    })

 
}

function mostrarMensaje(mensaje) {
    Swal.fire({
        icon: "success",
        title: mensaje,
        showConfirmButton: false,
        timer: 1500
    });
}




