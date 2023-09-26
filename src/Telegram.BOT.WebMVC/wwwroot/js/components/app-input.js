import { BaseElement } from './baseElement.js'

class AppInput extends BaseElement {
    inputValue
    constructor() {
        super()
        this.elementRoot.appendChild(this.build())
    }

    build() {
        let container = document.createElement("div")
        let label = document.createElement("label")
        let input = document.createElement("input")

        container.classList.add("mb-3")

        label.innerText = this.getAttribute("labelText")
        label.setAttribute("for", this.getAttribute("id"))
        label.classList.add("form-label")

        input.setAttribute("type", this.getAttribute("type") == null ? "text" : this.getAttribute("type"))
        input.setAttribute("type", this.getAttribute("id"))
        input.setAttribute("name", this.getAttribute("name"))
        input.classList.add("form-control")

        container.appendChild(label)
        container.appendChild(input)

        return container
    }

}
customElements.define("app-input", AppInput)