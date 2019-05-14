<?php
	var_dump(opcache_get_status());
	opcache_reset();
	echo "clean opcache";
	var_dump(opcache_get_status());	
?>
