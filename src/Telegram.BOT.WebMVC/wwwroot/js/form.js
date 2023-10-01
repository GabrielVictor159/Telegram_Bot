class Form {
    element
    constructor(form) {
        this.element = form
        this.element.addEventListener("submit", (event) => {
            if (this.hasEmpty() | !this.isNumeric() | !this.isEquals()) {
                event.preventDefault()
                this.addRealTimeContentValidation()
            }
        })
    }

    hasEmpty() {
        let groups = this.getValidableInputs()
        let result = false
        groups.forEach((group) => {
            let input = group.querySelector("input, textarea, select")
            if (input.value == "") {
                this.invalidate(group, "Campo obrigatório")
                result = true
            }
        })
        return result
    }

    isNumeric() {
        let groups = this.getValidableInputs().filter(element => element.classList.contains("numeric"))

        let result = true
        groups.forEach((group) => {
            let input = group.querySelector("input, textarea, select")

            if (isNaN(input.value) && input.value != "") {
                this.invalidate(group, "Campo deve conter apenas numeros.")
                result = false
            }
        })
        return result
    }

    isEquals() {
        let groups = this.getValidableInputs().filter(element => element.classList.contains("equal"))
        let values = groups.map(element => element.querySelector("input, textarea, select").value)
        let result = true

        let filtered = values.filter((element, index, arr) => {
            return arr.indexOf(element) == index
        })

        if (filtered.lenght != 1) {
            result = false
            groups.forEach(group => {
                this.invalidate(group, "Os campos devem ser iguais.")
            })
        }
        debugger
        return result
    }

    addRealTimeContentValidation() {
        let groups = this.getValidableInputs()
        groups.forEach(group => {
            let input = group.querySelector("input, textarea, select")

            input.addEventListener("input", (event) => {
                if (input.value == "") {
                    this.invalidate(group, "Campo obrigatório.")
                } else if (isNaN(input.value) && group.classList.contains("numeric")) {
                    this.invalidate(group, "Campo deve conter apenas numeros.")
                }
                else {
                    this.validate(group)
                }
            })
        })
    }

    getValidableInputs() {
        return Array.from(this.element.querySelectorAll("div")).filter(element => {
            return element.querySelector(".invalid-feedback") != null
        })
    }

    invalidate(group, message = "") {
        group.querySelector("input, textarea, select").classList.add("is-invalid")
        group.querySelector(".invalid-feedback").innerText = message
    }

    validate(group) {
        group.querySelector("input, textarea, select").classList.remove("is-invalid")
    }

    validateEmail(input) {
        let emailRegex = /\S+@\S+\.\S+/
        let divFeedback = document.querySelector(".email-feedback")
        if (emailRegex.test(input.value)) {
            input.classList.remove("is-invalid")
            return true
        } else {
            input.classList.add("is-invalid")
            divFeedback.innerText = "Insira um Email válido."
            return false
        }
    }
}