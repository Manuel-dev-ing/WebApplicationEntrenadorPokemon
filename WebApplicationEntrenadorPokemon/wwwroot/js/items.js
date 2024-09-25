console.log("ITEMS");

const listaItems = document.querySelector("#listaItems");

let offset = 0;
let limit = 10;


const arrayPaginaAnterior = []
const arrayPaginaSiguiente = []

var url = "https://pokeapi.co/api/v2/item?offset=" + offset + "&limit=" + limit;



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
       
        items(data)
        spinnerelement.remove();

        arrayPaginaAnterior.push(data.previous);
        paginaAnteriorItems()
    
        paginaSiguienteItems()
        arrayPaginaSiguiente.push(data.next);
    }
   
    //var data = await respuesta.json();
    //console.log("RESPUESTA: ")
    
}

buscarPokemon()

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

/*    var divspinner = document.querySelector('#div-spinner');

    var main = document.querySelector('#listaItems')

    var section = document.querySelector('#id-seccion');

    section.parentNode.insertBefore(divspinner, main);*/

}

function items(data) {
   
    data = data.results

    data.forEach((res) => mostrarItems(res))
}


async function paginaAnteriorItems() {

    var btnprevious = document.getElementById('previous');


    var previous = arrayPaginaAnterior[arrayPaginaAnterior.length - 1]

    previous = previous != null ? desabilitarBtnPrevious(false) : desabilitarBtnPrevious(true);
 
    btnprevious.addEventListener('click', async function () {
            crearSpinner()

            const spinnerelement = document.getElementById('div-spinner');

            var urlAnterior = arrayPaginaAnterior[arrayPaginaAnterior.length - 1]
            const divPoke = document.querySelectorAll('.item');
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
                
                previous = data.previous != null ? desabilitarBtnPrevious(false) : desabilitarBtnPrevious(true);
  
                arrayPaginaSiguiente.push(data.next);
                items(data);
                spinnerelement.remove();

            }
            /*const divPoke = document.querySelectorAll('.item');
            divPoke.forEach((divs) => divs.remove())
    
            const respuesta = await fetch(urlAnterior, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                }
            })
    
            if (respuesta.ok) {
    
                var data = await respuesta.json();
             
                items(data);
                console.log("DATA PAGINA SIGUIENTE : " + data.next + "PAGINA ANTERIOR: " + data.previous);
                console.log(data);
                

                if (urlAnterior == null) {
                    desabilitarBtnPrevious(true)

                }

                
            }*/
            /*items(data);
                   paginaSiguienteItems(data.next)
                   previous = data.previous*/
        })

   
  
}

function desabilitarBtnPrevious(bool) {
    var btnprevious = document.getElementById('previous')
    btnprevious.disabled = bool

}


async function paginaSiguienteItems() {

    var btnNext = document.getElementById('next');

    btnNext.addEventListener('click', async function () {
        crearSpinner()

        const spinnerelement = document.getElementById('div-spinner');

        var urlSiguiente = arrayPaginaSiguiente[arrayPaginaSiguiente.length - 1]

        const divPoke = document.querySelectorAll('.item');
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
            items(data);
            
            var previous = arrayPaginaAnterior[arrayPaginaAnterior.length - 1]
            if (previous != null) {
                desabilitarBtnPrevious(false)
            }
        }
        //este seria rest
 
        //console.log("DATA PAGINA SIGUIENTE: ");
        //console.log(data);
    })

}

async function mostrarItems(res) {

    var url = res.url;

    const respuesta = await fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })

    if (respuesta.ok) {
    
        var data = await respuesta.json();

        // Efecto del item
        var effectItem = data.effect_entries.map((ent) => ent.short_effect)

        var nombreItem = data.name;
        var imagenItem = data.sprites.default;

        const div = document.createElement("div");
        div.classList.add("item");
        div.innerHTML = `
                    <p class="item-id-back">#${data.id}</p>
                    <div class="item-imagen">
                        <img loading="lazy" src="${imagenItem}" alt="${nombreItem}" />
                    </div>

                    <div class="item-info">
                        <h2 class="item-nombre">${data.name}</h2>

                        <div class="nombre-contenedor">
                            <p class="text-description">Description:</p>
                            <p class="item-descripcion">${effectItem}</p>
                        </div>
                       
                        
                         <div>
                               <button class="btn btn-primary btnGuardar" id="${nombreItem}" >Guardar</button>
                         </div>
                    </div>
    `;

        listaItems.append(div);
        guardarItem(data)
    }
   
}

async function guardarItem(data) {

    var itemId = data.id;
    var nombreItem = data.name;
    var effectItem = data.effect_entries.map((ent) => ent.short_effect)[0];
    var imagenItem = data.sprites.default;

    const requestData = {
        id: itemId,
        nombre: nombreItem,
        descripcion: effectItem,
        imagen: imagenItem
    };


    var btnGuardar = document.getElementById(`${nombreItem}`)

    btnGuardar.addEventListener("click", async function () {
        var urlItem = "/api/item/item";

        console.log("click btn guardar Item: ");
        console.log(requestData);

        var datos = JSON.stringify(requestData)

        const respuesta = await fetch(urlItem, {
            method: 'POST',
            body: datos,
            headers: {
                'Content-Type': 'application/json'
            }

        })

        console.log("data: ")
        console.log(respuesta)

        if (respuesta.ok) {

            var mensaje = "Guardado exitosamente"

            mostrarMensaje(mensaje);
        }


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

