package com.pfr_crud.ssau.config;

import lombok.RequiredArgsConstructor;
import org.springframework.core.io.Resource;
import org.springframework.core.io.support.EncodedResource;
import org.springframework.core.io.support.PathMatchingResourcePatternResolver;
import org.springframework.jdbc.core.ConnectionCallback;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.datasource.init.ScriptUtils;
import org.springframework.stereotype.Component;

import javax.sql.DataSource;
import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.sql.Connection;
import java.sql.Statement;
import java.util.Arrays;
import java.util.Comparator;

@Component
@RequiredArgsConstructor
public class SqlScriptExecutor {
    private final DataSource dataSource;
    private final JdbcTemplate jdbcTemplate;

    public void executeAllFrom(String resourceFolder) throws IOException {
        PathMatchingResourcePatternResolver resolver = new PathMatchingResourcePatternResolver();
        Resource[] resources = resolver.getResources("classpath:" + resourceFolder + "/*.sql");

        Arrays.sort(resources, Comparator.comparing(Resource::getFilename));

        for (Resource resource : resources) {
            executeScript(resource);
        }
    }

    public void executeScript(Resource resource) {
        try {
            String sql = new String(resource.getInputStream().readAllBytes(), StandardCharsets.UTF_8);
            jdbcTemplate.execute(sql);
            System.out.println("Выполнился скрипт: " + resource.getFilename());
        } catch (Exception e) {
            throw new RuntimeException("Не получилось выполнить скрипт: " + resource.getFilename(), e);
        }
    }
}