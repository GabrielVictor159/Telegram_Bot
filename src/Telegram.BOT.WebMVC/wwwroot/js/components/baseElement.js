import { bootstrap } from '../../lib/bootstrap/dist/js/bootstrap.css.js'

export class BaseElement extends HTMLElement{
    elementRoot

    constructor() {
        super()
        this.elementRoot = this.attachShadow({ mode: "open" })
        this.elementRoot.appendChild(this.appendBootstrap())
    }

    appendBootstrap() {
        let style = document.createElement("style")
        style.innerText = bootstrap
        return style
    }
}