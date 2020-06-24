## Arquitectura de la Aplicación

La arquitectuda de la aplicación propuesta es un modelo de n-capas. [Más Informcaión](https://geeks.ms/jkpelaez/2009/05/30/arquitectura-basada-en-capas/)

## Visión Genera

<div style="text-align:center">

![][image_ref_a30ff4ads]

</div>

[image_ref_a30ff4ads]:https://github.com/patricioarena/Editores/blob/develop/Documentation/VisionGeneral.jpg

## Fabrica de contextos
<p>Tiene la responsabilidad de proveer el contexto requerido a cada uno de los servicios que requieran de
él.</p>

![][image_ref_a31ff4ads]

[image_ref_a31ff4ads]:https://github.com/patricioarena/Editores/blob/develop/Documentation/AbstractCotextFactory.jpg

## Fabrica de Servicios
<p>La razon para implementar este patron es poseer una clase que actue de proveedor de servicios comunes
que se utilizaran en todos los servicios pero que no sea necesario inyectarlos en cada nuevo servicio
que creemos.</p>
<div style="text-align:center">

![][image_ref_a32ff4ads]

</div>

[image_ref_a32ff4ads]:https://github.com/patricioarena/Editores/blob/develop/Documentation/AbstractServiceFactory.jpg

## Ejemplo de uso de las fabricas

<div style="text-align:center">

![][image_ref_a33ff4ads]

</div>

[image_ref_a33ff4ads]:https://github.com/patricioarena/Editores/blob/develop/Documentation/Example.jpg

---

### Más Documentación

- [*AngularCli →* ../EditoresDeTexto/README.md](https://github.com/patricioarena/Editores/tree/develop/EditoresDeTexto)
- [*ApplicationTest →* ..ApplicationTests/README.md](https://github.com/patricioarena/Editores/tree/develop/ApplicationTests)
