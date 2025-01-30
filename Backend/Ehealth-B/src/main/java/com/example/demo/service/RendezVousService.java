package com.example.demo.service;

import com.example.demo.entity.RendezVous;
import com.example.demo.repository.MedecinRepository;
import com.example.demo.repository.PatientRepository;
import com.example.demo.repository.RendezVousRepository;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class RendezVousService {

    @Autowired
    private RendezVousRepository rendezVousRepository;

    @Autowired
    private MedecinRepository medecinRepository;

    @Autowired
    private PatientRepository patientRepository;

    public RendezVous createOrUpdateRendezVous(RendezVous rendezVous) {
        // Vérifier que medecin et patient ne sont pas null
        if (rendezVous.getMedecin() == null || rendezVous.getPatient() == null) {
            throw new IllegalArgumentException("Le médecin et le patient doivent être spécifiés.");
        }

        // Vérifier que medecin et patient existent en base de données
        medecinRepository.findById(rendezVous.getMedecin().getId())
                .orElseThrow(() -> new IllegalArgumentException("Médecin non trouvé"));
        patientRepository.findById(rendezVous.getPatient().getId())
                .orElseThrow(() -> new IllegalArgumentException("Patient non trouvé"));

        return rendezVousRepository.save(rendezVous);
    }
    
    public RendezVous updateStatut(Long id, String statut) {
        RendezVous rendezVous = rendezVousRepository.findById(id)
                .orElseThrow(() -> new IllegalArgumentException("Rendez-vous non trouvé"));

        if (!statut.equals("CONFIRME") && !statut.equals("ANNULE") && !statut.equals("EN_ATTENTE")) {
            throw new IllegalArgumentException("Statut invalide. Les statuts possibles sont : CONFIRME, ANNULE, EN_ATTENTE.");
        }

        rendezVous.setStatut(statut);
        return rendezVousRepository.save(rendezVous);
    }

    public List<RendezVous> getAllRendezVous() {
        return rendezVousRepository.findAll();
    }

    public RendezVous getRendezVousById(Long id) {
        return rendezVousRepository.findById(id).orElse(null);
    }

    public void deleteRendezVous(Long id) {
        rendezVousRepository.deleteById(id);
    }
}