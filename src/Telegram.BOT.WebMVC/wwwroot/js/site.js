const validableForms = Array.from(document.getElementsByClassName("validate"))
if (validableForms != null) {
    validableForms.forEach(form => {
        let formValidator = new FormValidator(form);
        let isInvalid = false
        form.addEventListener("submit", event => {
            if (formValidator.allInputGroups.length > 0) {
                isInvalid = formValidator.hasEmpty()
            }
            if (formValidator.numericInputGroups.length > 0 && !isInvalid) {
                isInvalid = formValidator.hasInvalidNumbers()
            }
            if (isInvalid) {
                event.preventDefault()
                formValidator.addRealTimeContentValidation()
            }
        })
    })
}


