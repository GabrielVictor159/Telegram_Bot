class appForm extends HTMLElement {
    elementRoot
    constructor() {
        super()
        //this.elementRoot = this.attachShadow({ mode: "open" })
        this.elementRoot.appendChild(this.build())
    }

    build() {
        var input = document.createElement("input")
        input.type = "text"
        this.elementRoot.appendChild(input)
        return input
    }
}
customElements.define("form-input", AppInput)