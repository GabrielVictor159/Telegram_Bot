class FormValidator {
    formRoot
    allInputGroups
    numericInputGroups
    sizedInputGroups
    constructor(form) {
        this.formRoot = form
        this.allInputGroups = this.getValidableInputs()
        this.numericInputGroups = this.getValidableInputs().filter(element => element.classList.contains("numeric"))
        this.sizedInputGroups = this.getValidableInputs().filter(element => element.classList.contains("has-min-size"))
    }

    hasEmpty() {
        let result = false
        this.allInputGroups.forEach((group) => {
            let input = group.querySelector("input, textarea, select")
            if (input.value == "") {
                this.invalidate(group, "Campo obrigatório")
                result = true
            }
        })
        return result
    }

    hasInvalidNumbers() {
        let result = false
        if (this.numericInputGroups.length > 0) {
            this.numericInputGroups.forEach((group) => {
                let input = group.querySelector("input, textarea, select")

                if (isNaN(input.value) && input.value != "") {
                    this.invalidate(group, "Campo deve conter apenas numeros.")
                    result = true
                }
            })
        }
        return result
    }

    hasInvalidSize() {
        let result = false
        this.sizedInputGroups.forEach(group => {
            let input = group.querySelector("input, textarea, select")
            let minSize = Number.parseInt(input.getAttribute("minTextSize"))
            if (input.value.length < minSize) {
                this.invalidate(group, `O campo deve conter no mínimo ${minSize} caracteres.`)
                result = true
            }
        })
        return result
    }

    addRealTimeContentValidation() {
        this.allInputGroups.forEach(group => {
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
        let groups = Array.from(this.formRoot.querySelectorAll("div")).filter(element => {
            return element.querySelector(".invalid-feedback") != null
        }).filter(group => !group.classList.contains("no-validate"))
        return groups == null ? [] : groups
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