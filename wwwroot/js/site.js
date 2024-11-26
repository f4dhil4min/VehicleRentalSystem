<script>
    function populateForm(name, type, rate) {
        document.getElementById('VehicleName').value = name;
    document.getElementById('VehicleType').value = type;
    document.getElementById('RentalRate').value = rate;
    window.scrollTo({
        top: document.getElementById('VehicleName').offsetTop - 100,
    behavior: 'smooth'
        });
    }
</script>

