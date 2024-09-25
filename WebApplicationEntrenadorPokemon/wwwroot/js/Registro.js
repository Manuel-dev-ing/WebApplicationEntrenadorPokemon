console.log("HOLA MUNDO");


var num = generarIdEntrenador();

document.getElementById('IDEntrenador').value = num;
console.log("Tipo de dato ", typeof(num));
console.log("numero es: ", num);




function generarIdEntrenador() {
    var min = 100000;
    var max = 900000;

    const num = Math.floor(min + Math.random() * max);

    return num
}





