package com.example.demo.controller;

import com.example.demo.entity.RendezVous;
import com.example.demo.service.RendezVousService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/rendezvous")
//@CrossOrigin(origins = "http://localhost:3000")
public class RendezVousController {

    @Autowired
    private RendezVousService rendezVousService;

    @GetMapping
    public List<RendezVous> getAllRendezVous() {
        return rendezVousService.getAllRendezVous();
    }

    @GetMapping("/{id}")
    public ResponseEntity<?> getRendezVousById(@PathVariable Long id) {
        RendezVous rendezVous = rendezVousService.getRendezVousById(id);
        if (rendezVous != null) {
            return ResponseEntity.ok(rendezVous);
        } else {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body("Rendez-vous non trouvé");
        }
    }

    @PostMapping
    public ResponseEntity<?> createOrUpdateRendezVous(@RequestBody RendezVous rendezVous) {
        try {
            RendezVous savedRendezVous = rendezVousService.createOrUpdateRendezVous(rendezVous);
            return ResponseEntity.ok(savedRendezVous);
        } catch (IllegalArgumentException e) {
            return ResponseEntity.badRequest().body(e.getMessage());
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("Une erreur est survenue.");
        }
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<?> deleteRendezVous(@PathVariable Long id) {
        try {
            rendezVousService.deleteRendezVous(id);
            return ResponseEntity.ok("Rendez-vous supprimé avec succès");
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("Erreur lors de la suppression du rendez-vous");
        }
    }
}