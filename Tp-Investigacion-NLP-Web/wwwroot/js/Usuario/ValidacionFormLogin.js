document.addEventListener('DOMContentLoaded', () => {

    const form = document.querySelector('form');

    form.addEventListener('submit', e => {

        let valido = true;

        const email = document.getElementById('Email');
        const password = document.getElementById('Password');

        document.querySelector('[data-valmsg-for="Email"]').textContent = '';
        document.querySelector('[data-valmsg-for="Password"]').textContent = '';

        // Email
        if (email.value.trim() === '') {

            document.querySelector('[data-valmsg-for="Email"]')
                .textContent = 'El email es obligatorio.';

            valido = false;
        }
        else if (email.value.length > 50) {

            document.querySelector('[data-valmsg-for="Email"]')
                .textContent = 'El email no puede tener más de 50 caracteres.';

            valido = false;
        }
        else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email.value)) {

            document.querySelector('[data-valmsg-for="Email"]')
                .textContent = 'Ingrese un email válido.';

            valido = false;
        }

        // Password
        if (password.value.trim() === '') {

            document.querySelector('[data-valmsg-for="Password"]')
                .textContent = 'La contraseña es obligatoria.';

            valido = false;
        }
        else if (password.value.length > 50) {

            document.querySelector('[data-valmsg-for="Password"]')
                .textContent = 'La contraseña no puede tener más de 50 caracteres.';

            valido = false;
        }

        if (!valido) {
            e.preventDefault();
        }

    });

});