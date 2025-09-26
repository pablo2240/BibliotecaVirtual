
    // Configuración de la API
    const API_BASE_URL = 'https://localhost:7272/api'; // Cambiar por tu URL de API

        // Variables globales
        let currentUser = null;

        // Función para cambiar de tab
        function showTab(tabName) {
            // Ocultar todos los tabs
            const tabs = document.querySelectorAll('.tab-content');
            tabs.forEach(tab => tab.classList.remove('active'));

            // Ocultar todos los botones activos
            const buttons = document.querySelectorAll('.tab-btn');
            buttons.forEach(btn => btn.classList.remove('active'));

            // Mostrar tab seleccionado
            document.getElementById(tabName).classList.add('active');
            event.target.classList.add('active');

            // Cargar contenido específico del tab
            if (tabName === 'catalogo') {
                cargarCatalogo();
            } else if (tabName === 'reportes') {
                cargarReporte();
            } else if (tabName === 'prestamos') {
                updatePrestamoSection();
            }
        }

        // Función para mostrar mensajes
        function showMessage(elementId, message, isSuccess = true) {
            const element = document.getElementById(elementId);
            element.innerHTML = `<div class="message ${isSuccess ? 'success' : 'error'}">${message}</div>`;
            setTimeout(() => {
                element.innerHTML = '';
            }, 5000);
        }

        // RF2: Iniciar Sesión
        document.getElementById('loginForm').addEventListener('submit', async (e) => {
            e.preventDefault();
            
            const email = document.getElementById('loginEmail').value;
            const password = document.getElementById('loginPassword').value;

            try {
                const response = await fetch(`${API_BASE_URL}/usuarios/login`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify({
                        correo: email,
                        contrasena: password
                    })
                });

                if (response.ok) {
                    const user = await response.json();
                    currentUser = user;
                    localStorage.setItem('currentUser', JSON.stringify(user));
                    
                    showMessage('loginMessage', '✅ Inicio de sesión exitoso');
                    updatePrestamoSection();
                    
                    // Limpiar formulario
                    document.getElementById('loginForm').reset();
                } else {
                    showMessage('loginMessage', '❌ Credenciales inválidas', false);
                }
            } catch (error) {
                showMessage('loginMessage', '❌ Error de conexión con el servidor', false);
                console.error('Error:', error);
            }
        });

        // RF1: Registro de Usuario
        document.getElementById('registroForm').addEventListener('submit', async (e) => {
            e.preventDefault();
            
            const nombre = document.getElementById('registroNombre').value;
            const email = document.getElementById('registroEmail').value;
            const tipo = parseInt(document.getElementById('registroTipo').value);
            const password = document.getElementById('registroPassword').value;

            try {
                const response = await fetch(`${API_BASE_URL}/usuarios/registro`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify({
                        nombre: nombre,
                        correo: email,
                        contrasena: password,
                        tipo: tipo
                    })
                });

                if (response.ok) {
                    showMessage('registroMessage', '✅ Usuario registrado exitosamente');
                    document.getElementById('registroForm').reset();
                } else {
                    const errorText = await response.text();
                    showMessage('registroMessage', `❌ ${errorText}`, false);
                }
            } catch (error) {
                showMessage('registroMessage', '❌ Error de conexión con el servidor', false);
                console.error('Error:', error);
            }
        });

        // RF3: Consultar Catálogo
        document.getElementById('busquedaForm').addEventListener('submit', async (e) => {
            e.preventDefault();
            cargarCatalogo();
        });

        async function cargarCatalogo() {
            const titulo = document.getElementById('buscarTitulo').value;
            const autor = document.getElementById('buscarAutor').value;
            const categoria = document.getElementById('buscarCategoria').value;

            // Construir query parameters
            const params = new URLSearchParams();
            if (titulo) params.append('titulo', titulo);
            if (autor) params.append('autor', autor);
            if (categoria) params.append('categoria', categoria);

            try {
                document.getElementById('catalogoResults').innerHTML = `
                    <div class="loading">
                        <div class="spinner"></div>
                        <p>Buscando libros...</p>
                    </div>
                `;

                const response = await fetch(`${API_BASE_URL}/libros/catalogo?${params}`);
                
                if (response.ok) {
                    const libros = await response.json();
                    mostrarLibros(libros);
                } else {
                    document.getElementById('catalogoResults').innerHTML = 
                        '<div class="message error">❌ Error al cargar el catálogo</div>';
                }
            } catch (error) {
                document.getElementById('catalogoResults').innerHTML = 
                    '<div class="message error">❌ Error de conexión con el servidor</div>';
                console.error('Error:', error);
            }
        }

        function mostrarLibros(libros) {
            const container = document.getElementById('catalogoResults');
            
            if (libros.length === 0) {
                container.innerHTML = '<div class="message">📚 No se encontraron libros con esos criterios</div>';
                return;
            }

            const librosHTML = libros.map(libro => {
                const estadoClass = {
                    1: 'status-disponible',
                    2: 'status-prestado',
                    3: 'status-reservado'
                }[libro.estado];

                const estadoTexto = {
                    1: 'Disponible',
                    2: 'Prestado',
                    3: 'Reservado'
                }[libro.estado];

                return `
                    <div class="book-card">
                        <div class="book-title">${libro.titulo}</div>
                        <div class="book-info"><strong>Autor:</strong> ${libro.autor}</div>
                        <div class="book-info"><strong>Categoría:</strong> ${libro.categoria}</div>
                        <div class="book-info"><strong>ID:</strong> ${libro.id}</div>
                        <span class="book-status ${estadoClass}">${estadoTexto}</span>
                    </div>
                `;
            }).join('');

            container.innerHTML = `
                <h3>📚 Resultados (${libros.length} libros encontrados)</h3>
                <div class="book-grid">${librosHTML}</div>
            `;
        }

        // RF4: Reservar Libro
        document.getElementById('reservaForm').addEventListener('submit', async (e) => {
            e.preventDefault();
            
            if (!currentUser) {
                showMessage('prestamosMessage', '❌ Debes iniciar sesión primero', false);
                return;
            }

            const libroId = parseInt(document.getElementById('libroIdReserva').value);

            try {
                const response = await fetch(`${API_BASE_URL}/prestamos/reservar`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify({
                        libroId: libroId,
                        usuarioId: currentUser.id
                    })
                });

                if (response.ok) {
                    showMessage('prestamosMessage', '✅ Libro reservado exitosamente');
                    document.getElementById('reservaForm').reset();
                } else {
                    const errorText = await response.text();
                    showMessage('prestamosMessage', `❌ ${errorText}`, false);
                }
            } catch (error) {
                showMessage('prestamosMessage', '❌ Error de conexión con el servidor', false);
                console.error('Error:', error);
            }
        });

        // RF5: Prestar Libro
        document.getElementById('prestamoForm').addEventListener('submit', async (e) => {
            e.preventDefault();
            
            const libroId = parseInt(document.getElementById('libroIdPrestamo').value);

            try {
                const response = await fetch(`${API_BASE_URL}/prestamos/${libroId}/prestar`, {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json',
                    }
                });

                if (response.ok) {
                    showMessage('prestamosMessage', '✅ Libro prestado exitosamente');
                    document.getElementById('prestamoForm').reset();
                } else {
                    const errorText = await response.text();
                    showMessage('prestamosMessage', `❌ ${errorText}`, false);
                }
            } catch (error) {
                showMessage('prestamosMessage', '❌ Error de conexión con el servidor', false);
                console.error('Error:', error);
            }
        });

        // RF6: Devolver Libro
        document.getElementById('devolucionForm').addEventListener('submit', async (e) => {
            e.preventDefault();
            
            const prestamoId = parseInt(document.getElementById('prestamoIdDevolucion').value);

            try {
                const response = await fetch(`${API_BASE_URL}/prestamos/${prestamoId}/devolver`, {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json',
                    }
                });

                if (response.ok) {
                    showMessage('prestamosMessage', '✅ Libro devuelto exitosamente');
                    document.getElementById('devolucionForm').reset();
                } else {
                    const errorText = await response.text();
                    showMessage('prestamosMessage', `❌ ${errorText}`, false);
                }
            } catch (error) {
                showMessage('prestamosMessage', '❌ Error de conexión con el servidor', false);
                console.error('Error:', error);
            }
        });

        // RF7: Generar Reporte
        async function cargarReporte() {
            try {
                document.getElementById('reporteResults').innerHTML = `
                    <div class="loading">
                        <div class="spinner"></div>
                        <p>Generando reporte...</p>
                    </div>
                `;

                const response = await fetch(`${API_BASE_URL}/prestamos/reporte-activos`);
                
                if (response.ok) {
                    const prestamos = await response.json();
                    mostrarReporte(prestamos);
                } else {
                    document.getElementById('reporteResults').innerHTML = 
                        '<div class="message error">❌ Error al generar el reporte</div>';
                }
            } catch (error) {
                document.getElementById('reporteResults').innerHTML = 
                    '<div class="message error">❌ Error de conexión con el servidor</div>';
                console.error('Error:', error);
            }
        }

        function mostrarReporte(prestamos) {
            const container = document.getElementById('reporteResults');
            
            if (prestamos.length === 0) {
                container.innerHTML = '<div class="message">📋 No hay préstamos activos en este momento</div>';
                return;
            }

            const prestamosHTML = prestamos.map(prestamo => `
                <tr>
                    <td>${prestamo.id}</td>
                    <td>${prestamo.usuarioNombre}</td>
                    <td>${prestamo.libroTitulo}</td>
                    <td>${new Date(prestamo.fechaInicio).toLocaleDateString()}</td>
                    <td><span class="book-status status-prestado">Activo</span></td>
                </tr>
            `).join('');

            container.innerHTML = `
                <h3>📊 Préstamos Activos (${prestamos.length})</h3>
                <table class="report-table">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Usuario</th>
                            <th>Libro</th>
                            <th>Fecha Préstamo</th>
                            <th>Estado</th>
                        </tr>
                    </thead>
                    <tbody>
                        ${prestamosHTML}
                    </tbody>
                </table>
            `;
        }

        // Función para actualizar sección de préstamos
        function updatePrestamoSection() {
            if (currentUser) {
                document.getElementById('userSection').style.display = 'block';
                document.getElementById('loginPrompt').style.display = 'none';
                document.getElementById('currentUserInfo').textContent = 
                    `${currentUser.nombre} (${currentUser.tipo === 1 ? 'Estudiante' : 'Docente'})`;
            } else {
                document.getElementById('userSection').style.display = 'none';
                document.getElementById('loginPrompt').style.display = 'block';
            }
        }

        // Función para cerrar sesión
        function logout() {
            currentUser = null;
            localStorage.removeItem('currentUser');
            updatePrestamoSection();
            showMessage('prestamosMessage', '✅ Sesión cerrada exitosamente');
        }

        // Inicializar la aplicación
        document.addEventListener('DOMContentLoaded', () => {
            // Recuperar usuario de localStorage
            const savedUser = localStorage.getItem('currentUser');
            if (savedUser) {
                currentUser = JSON.parse(savedUser);
                updatePrestamoSection();
            }

            // Cargar catálogo inicial
            cargarCatalogo();
            cargarReporte();
        });